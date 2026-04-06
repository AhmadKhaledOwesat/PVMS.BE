using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Hosting;
using PVMS.Application.Dto;
using PVMS.Application.Interfaces;
using System.Text;
using Path = System.IO.Path;

namespace PVMS.Application.Services
{
    public class WordService(IHostEnvironment webHostEnvironment) : IWordService
    {
        void ReplaceSplitPlaceholders(WordprocessingDocument wordDoc, List<PlaceHolder> placeHolders)
        {
            var doc = wordDoc.MainDocumentPart.Document;

            foreach (var element in doc.ChildElements)
            {
                if (element is Paragraph para)
                {
                    var runs = para.ChildElements.OfType<Run>().ToList();
                    int i = 0;

                    while (i < runs.Count)
                    {
                        var run = runs[i];
                        var textNode = run.ChildElements.OfType<Text>().FirstOrDefault();
                        if (textNode == null)
                        {
                            i++;
                            continue;
                        }

                        // Check if this text starts a placeholder
                        if (textNode.Text.Contains("{{"))
                        {
                            StringBuilder placeholderBuilder = new StringBuilder();
                            List<Text> nodesToRemove = new List<Text>();
                            bool endFound = false;

                            // Loop through runs and merge text until we find '}}'
                            for (int j = i; j < runs.Count; j++)
                            {
                                var r = runs[j];
                                var t = r.ChildElements.OfType<Text>().FirstOrDefault();
                                if (t != null)
                                {
                                    placeholderBuilder.Append(t.Text);
                                    nodesToRemove.Add(t);

                                    if (t.Text.Contains("}}"))
                                    {
                                        endFound = true;
                                        break;
                                    }
                                }
                            }

                            if (endFound)
                            {
                                string mergedText = placeholderBuilder.ToString();

                                // Find matching placeholder
                                var ph = placeHolders.FirstOrDefault(p => mergedText.Contains(p.Key));
                                if (ph != null)
                                {
                                    // Remove old Text nodes
                                    foreach (var t in nodesToRemove)
                                        t.Remove();

                                    // Insert new merged Text node with replacement
                                    var newRun = new Run(new Text(ph.Value.ToString()) { Space = SpaceProcessingModeValues.Preserve });
                                    para.InsertBefore(newRun, run);
                                }
                            }

                            // Move index past processed runs
                            i += nodesToRemove.Count;
                        }
                        else
                        {
                            i++;
                        }
                    }
                }
            }

            doc.Save();
        }
        public async Task ReplaceTextAsync(string ticketId, List<PlaceHolder> placeHolders)
        {
            string wwwroot = Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot");

            string filePath = Path.Combine(wwwroot, "template", "Violation_Template.docx");

            byte[] fileBytes = await File.ReadAllBytesAsync(filePath);

            using var doc = WordprocessingDocument.Open(new MemoryStream(fileBytes), true);


            // iterate through top-level elements
            foreach (var element in doc.MainDocumentPart.Document.Body.ChildElements)
            {
                if (element is Table table)
                {
                    foreach (var row in table.ChildElements.OfType<TableRow>())
                    {
                        foreach (var cell in row.ChildElements.OfType<TableCell>())
                        {
                            foreach (var paraInCell in cell.ChildElements.OfType<Paragraph>())
                            {
                                foreach (var run in paraInCell.ChildElements.OfType<Run>())
                                {
                                    foreach (var text in run.ChildElements.OfType<Text>())
                                    {
                                        if (text.Text.Contains("{{") || text.Text.Contains("}}"))
                                        {
                                            if (placeHolders.FirstOrDefault(a => a.Key == text.Text) is PlaceHolder ph)
                                            {
                                                text.Text = text.Text.Replace(ph.Key, ph.Value.ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (element is Paragraph para)
                {
                    foreach (var run in para.ChildElements.OfType<Run>())
                    {
                        foreach (var text in run.ChildElements.OfType<Text>())
                        {
                            if (text.Text.Contains("{{") || text.Text.Contains("}}"))
                            {
                                if (placeHolders.FirstOrDefault(a => a.Key == element.InnerText) is PlaceHolder ph)
                                {
                                    text.Text = ph.Value.ToString();
                                }
                            }
                        }
                    }
                }
            }
            doc.MainDocumentPart.Document.Save();

            new MemoryStream(fileBytes).Position = 0;

            // ✅ Output path (example)
            string outputPath = Path.Combine(
                wwwroot,
                "generated",
                $"Violation_{ticketId}.docx"
            );

            Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);

            await using var fileStream = new FileStream(
    outputPath,
    FileMode.Create,
    FileAccess.Write
);

            await new MemoryStream(fileBytes).CopyToAsync(fileStream);

        }
    }
}
