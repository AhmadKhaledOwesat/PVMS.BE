

namespace PVMS.Domain.Interfaces
{
    public interface IInnovaMapper
    {
        TDestination Map<TSource , TDestination>(TSource source);
        void Map<TSource, TDestination>(TSource source , TDestination destination);
        TDestination Map<TDestination>(object source);
    }
}
