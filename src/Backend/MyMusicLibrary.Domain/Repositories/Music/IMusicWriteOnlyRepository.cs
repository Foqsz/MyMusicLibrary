namespace MyMusicLibrary.Domain.Repositories.Music;
public interface IMusicWriteOnlyRepository
{
    Task Add(Entities.Music music);
    Task Delete(long musicId);
}
