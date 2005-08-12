using System;
using System.Drawing;

namespace OCR
{
    public class ImageRecognizer
    {
        //Felder
        private OCREngine engine;
        private Bitmap image;

        private byte contrastRefValue;
        private sbyte imageNoiseSphere;
        private float imageNoiseRefValue;


        #region Eigenschaften
        public OCREngine Engine
        {
            get { return engine; }
            set { engine = value; }
        }

        public Bitmap Image
        {
            get { return image; }
            set { image = value; }
        }

        public byte ContrastRefValue
        {
            get { return contrastRefValue; }
            set { contrastRefValue = value; }
        }

        public sbyte ImageNoiseSphere
        {
            get { return imageNoiseSphere; }
            set { imageNoiseSphere = value; }
        }

        public float ImageNoiseRefValue
        {
            get { return imageNoiseRefValue; }
            set { imageNoiseRefValue = value; }
        }
        #endregion

        #region Konstruktoren
        public ImageRecognizer()
        {
            this.Engine = null;
            this.Image = null;

            contrastRefValue = 0;
            imageNoiseSphere = 0;
            imageNoiseRefValue = 0;
        }

        public ImageRecognizer(OCREngine Engine)
        {
            this.Engine = Engine;
            this.Image = null;

            contrastRefValue = 0;
            imageNoiseSphere = 0;
            imageNoiseRefValue = 0;
        }

        public ImageRecognizer(OCREngine Engine, Image Image)
        {
            this.Engine = Engine;
            this.Image = new Bitmap(Image);

            contrastRefValue = 0;
            imageNoiseSphere = 0;
            imageNoiseRefValue = 0;
        }
        #endregion

        //Methoden
        public void ImproveImage()
        {
            if (this.Image == null)
                throw new MissingFieldException("Kein Bild eingegeben!");
            if (contrastRefValue == 0 || imageNoiseSphere == 0 || imageNoiseRefValue == 0)
                throw new MissingFieldException("Flasche Parameter!");

            this.Image = ImageImprovment.Contrast(this.Image, ContrastRefValue);
            this.Image = ImageImprovment.ImageNoise(this.Image, ImageNoiseSphere, ImageNoiseRefValue);
            this.Image = ImageImprovment.Cut(this.Image);
        }

        public string Recognize()
        {
            if (this.Engine == null)
                throw new MissingFieldException("Keine OCR Engine geladen!");
            if (this.Image == null)
                throw new MissingFieldException("Kein Bild eingegeben!");

            string s = "test";
            return s;
            

        }
    }
}
