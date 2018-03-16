using System;
using System.IO;

namespace gfoidl.ConfigBuilders
{
    internal static class PathExtensions
    {
        public static string InsertMachineName(this string path)
        {
            var pathSegments      = new PathSegments(path);
            pathSegments.FileName = $"{pathSegments.FileName}_{Environment.MachineName}";

            return pathSegments.GetFullPath();
        }
        //---------------------------------------------------------------------
        internal struct PathSegments
        {
            public string Folders   { get; set; }
            public string FileName  { get; set; }
            public string Extension { get; set; }
            //-----------------------------------------------------------------
            public PathSegments(string path)
            {
                this.Folders   = Path.GetDirectoryName(path);
                this.FileName  = Path.GetFileNameWithoutExtension(path);
                this.Extension = Path.GetExtension(path);
            }
            //-----------------------------------------------------------------
            public string GetFullPath() => Path.Combine(this.Folders, Path.ChangeExtension(this.FileName, this.Extension));
        }
    }
}
