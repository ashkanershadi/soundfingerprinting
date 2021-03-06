namespace SoundFingerprinting
{
    using System.Collections.Generic;

    using SoundFingerprinting.DAO;
    using SoundFingerprinting.Data;

    public interface IModelService
    {
        IModelReference InsertFingerprint(FingerprintData fingerprint);

        IModelReference InsertTrack(TrackData track);

        void InsertHashDataForTrack(IEnumerable<HashData> hashes, IModelReference trackReference);

        IList<HashData> ReadHashDataByTrack(IModelReference trackReference);
            
        IList<TrackData> ReadAllTracks();

        IList<TrackData> ReadTrackByArtistAndTitleName(string artist, string title);

        IList<FingerprintData> ReadFingerprintsByTrackReference(IModelReference trackReference);
            
        TrackData ReadTrackByReference(IModelReference trackReference);

        TrackData ReadTrackByISRC(string isrc);

        int DeleteTrack(IModelReference trackReference);

        IList<SubFingerprintData> ReadSubFingerprintDataByHashBucketsWithThreshold(long[] buckets, int threshold);

        IList<SubFingerprintData> ReadSubFingerprintDataByHashBucketsThresholdWithGroupId(long[] buckets, int threshold, string trackGroupId);
    }
}
