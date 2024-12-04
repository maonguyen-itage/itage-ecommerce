using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.CommonModels
{
    public class ImageFileInfo
    {
        public int AttachmentId { get; set; }
        public string? ImageName { get; set; }
        public string? ImageGuidName { get; set; }
        public string? ImageURL { get; set; }
    }
}
