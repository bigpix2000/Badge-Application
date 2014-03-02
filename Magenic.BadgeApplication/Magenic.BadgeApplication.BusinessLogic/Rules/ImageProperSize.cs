﻿using Csla.Core;
using Csla.Rules;
using System;
using System.Collections.Generic;
using System.IO;

namespace Magenic.BadgeApplication.BusinessLogic.Rules
{
    public class ImageProperSize : BusinessRule
    {
        private IPropertyInfo ImageProptery { get; set; }

        public ImageProperSize(IPropertyInfo imagePathProperty, IPropertyInfo imageProperty)
            : base(imagePathProperty)
        {

            if (imagePathProperty == null)
            {
                throw new ArgumentException("imagePathProperty cannot be null.");
            }
            if (imagePathProperty.Type != typeof(string))
            {
                throw new ArgumentException("imagePathProperty must be a string.");
            }
            if (imageProperty == null)
            {
                throw new ArgumentException("imageProperty cannot be null.");
            }
            if (imageProperty.Type != typeof(byte[]))
            {
                throw new ArgumentException("imageProperty must be a byte[].");
            }

            this.ImageProptery = imageProperty;
            this.InputProperties = new List<IPropertyInfo> { imagePathProperty, imageProperty };
        }

        protected override void Execute(RuleContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("Context cannot be null");
            }

            var imageArray = (byte[])context.InputPropertyValues[this.ImageProptery];
            if (imageArray != null && imageArray.Length > 0)
            {
                try
                {
                    using (var ms = new MemoryStream(imageArray))
                    {
                        var image = System.Drawing.Image.FromStream(ms);
                        if (image.Height != 100 || image.Width != 85)
                        {
                            context.AddErrorResult("The supplied image must have a height of 100 and a width of 85.");
                        }
                    }
                }
                catch (ArgumentException)
                {
                    context.AddErrorResult("Image must be set with a valid image type.");
                }
            }
        }
    }
}
