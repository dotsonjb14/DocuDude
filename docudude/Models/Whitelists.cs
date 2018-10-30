using System.Collections.Generic;

namespace docudude.Models
{
    public enum WhiteListType
    {
        PDF,
        Image
    }

    public class Whitelists
    {
        public List<string> PDFBuckets { get; set; } = new List<string>();
        public List<string> ImageBuckets { get; set; } = new List<string>();

        public bool CanAccess(string bucket, WhiteListType whiteListType)
        {
            List<string> list = GetBucketList(whiteListType);

            return list.Count == 0 || list.Contains(bucket);
        }

        public void SetWhiteList(string rawList, WhiteListType whiteListType)
        {
            List<string> list = GetBucketList(whiteListType);
            if(!string.IsNullOrWhiteSpace(rawList))
            {
                list.AddRange((rawList ?? "").Split("|"));
            }
        }

        private List<string> GetBucketList(WhiteListType bucketType)
        {
            if (bucketType == WhiteListType.PDF)
            {
                return PDFBuckets;
            }
            else if (bucketType == WhiteListType.Image)
            {
                return ImageBuckets;
            }

            return null;
        }
    }
}