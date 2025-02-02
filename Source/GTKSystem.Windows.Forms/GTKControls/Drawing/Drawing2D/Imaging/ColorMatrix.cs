using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
    /// <summary>Defines a 5 x 5 matrix that contains the coordinates for the RGBAW space. Several methods of the <see cref="T:System.Drawing.Imaging.ImageAttributes" /> class adjust image colors by using a color matrix. This class cannot be inherited.</summary>
    [StructLayout(LayoutKind.Sequential)]
    public sealed class ColorMatrix
    {
        private float _matrix00;

        private float _matrix01;

        private float _matrix02;

        private float _matrix03;

        private float _matrix04;

        private float _matrix10;

        private float _matrix11;

        private float _matrix12;

        private float _matrix13;

        private float _matrix14;

        private float _matrix20;

        private float _matrix21;

        private float _matrix22;

        private float _matrix23;

        private float _matrix24;

        private float _matrix30;

        private float _matrix31;

        private float _matrix32;

        private float _matrix33;

        private float _matrix34;

        private float _matrix40;

        private float _matrix41;

        private float _matrix42;

        private float _matrix43;

        private float _matrix44;

        /// <summary>Gets or sets the element at the 0 (zero) row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the 0 row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix00
        {
            get { return _matrix00; }
            set { _matrix00 = value; }
        }

        /// <summary>Gets or sets the element at the 0 (zero) row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the 0 row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" /> .</returns>
        public float Matrix01
        {
            get { return _matrix01; }
            set { _matrix01 = value; }
        }

        /// <summary>Gets or sets the element at the 0 (zero) row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the 0 row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix02
        {
            get { return _matrix02; }
            set { _matrix02 = value; }
        }

        /// <summary>Gets or sets the element at the 0 (zero) row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />. Represents the alpha component.</summary>
        /// <returns>The element at the 0 row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix03
        {
            get { return _matrix03; }
            set { _matrix03 = value; }
        }

        /// <summary>Gets or sets the element at the 0 (zero) row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the 0 row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix04
        {
            get { return _matrix04; }
            set { _matrix04 = value; }
        }

        /// <summary>Gets or sets the element at the first row and 0 (zero) column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the first row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix10
        {
            get { return _matrix10; }
            set { _matrix10 = value; }
        }

        /// <summary>Gets or sets the element at the first row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the first row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix11
        {
            get { return _matrix11; }
            set { _matrix11 = value; }
        }

        /// <summary>Gets or sets the element at the first row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the first row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix12
        {
            get { return _matrix12; }
            set { _matrix12 = value; }
        }

        /// <summary>Gets or sets the element at the first row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />. Represents the alpha component.</summary>
        /// <returns>The element at the first row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix13
        {
            get { return _matrix13; }
            set { _matrix13 = value; }
        }

        /// <summary>Gets or sets the element at the first row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the first row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix14
        {
            get { return _matrix14; }
            set { _matrix14 = value; }
        }

        /// <summary>Gets or sets the element at the second row and 0 (zero) column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the second row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix20
        {
            get { return _matrix20; }
            set { _matrix20 = value; }
        }

        /// <summary>Gets or sets the element at the second row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the second row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix21
        {
            get { return _matrix21; }
            set { _matrix21 = value; }
        }

        /// <summary>Gets or sets the element at the second row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the second row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix22
        {
            get { return _matrix22; }
            set { _matrix22 = value; }
        }

        /// <summary>Gets or sets the element at the second row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the second row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix23
        {
            get { return _matrix23; }
            set { _matrix23 = value; }
        }

        /// <summary>Gets or sets the element at the second row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the second row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix24
        {
            get { return _matrix24; }
            set { _matrix24 = value; }
        }

        /// <summary>Gets or sets the element at the third row and 0 (zero) column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the third row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix30
        {
            get { return _matrix30; }
            set { _matrix30 = value; }
        }

        /// <summary>Gets or sets the element at the third row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the third row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix31
        {
            get { return _matrix31; }
            set { _matrix31 = value; }
        }

        /// <summary>Gets or sets the element at the third row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the third row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix32
        {
            get { return _matrix32; }
            set { _matrix32 = value; }
        }

        /// <summary>Gets or sets the element at the third row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />. Represents the alpha component.</summary>
        /// <returns>The element at the third row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix33
        {
            get { return _matrix33; }
            set { _matrix33 = value; }
        }

        /// <summary>Gets or sets the element at the third row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the third row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix34
        {
            get { return _matrix34; }
            set { _matrix34 = value; }
        }

        /// <summary>Gets or sets the element at the fourth row and 0 (zero) column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the fourth row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix40
        {
            get { return _matrix40; }
            set { _matrix40 = value; }
        }

        /// <summary>Gets or sets the element at the fourth row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the fourth row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix41
        {
            get { return _matrix41; }
            set { _matrix41 = value; }
        }

        /// <summary>Gets or sets the element at the fourth row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the fourth row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix42
        {
            get { return _matrix42; }
            set { _matrix42 = value; }
        }

        /// <summary>Gets or sets the element at the fourth row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />. Represents the alpha component.</summary>
        /// <returns>The element at the fourth row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix43
        {
            get { return _matrix43; }
            set { _matrix43 = value; }
        }

        /// <summary>Gets or sets the element at the fourth row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <returns>The element at the fourth row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
        public float Matrix44
        {
            get { return _matrix44; }
            set { _matrix44 = value; }
        }

        /// <summary>Gets or sets the element at the specified row and column in the <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
        /// <param name="row">The row of the element.</param>
        /// <param name="column">The column of the element.</param>
        /// <returns>The element at the specified row and column.</returns>
        public float this[int row, int column]
        {
            get { return GetMatrix()[row][column]; }
            set
            {
                float[][] matrix = GetMatrix();
                matrix[row][column] = value;
                SetMatrix(matrix);
            }
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ColorMatrix" /> class.</summary>
        public ColorMatrix()
        {
            _matrix00 = 1f;
            _matrix11 = 1f;
            _matrix22 = 1f;
            _matrix33 = 1f;
            _matrix44 = 1f;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ColorMatrix" /> class using the elements in the specified matrix <paramref name="newColorMatrix" />.</summary>
        /// <param name="newColorMatrix">The values of the elements for the new <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</param>
        [CLSCompliant(false)]
        public ColorMatrix(float[][] newColorMatrix)
        {
            SetMatrix(newColorMatrix);
        }

        internal void SetMatrix(float[][] newColorMatrix)
        {
            _matrix00 = newColorMatrix[0][0];
            _matrix01 = newColorMatrix[0][1];
            _matrix02 = newColorMatrix[0][2];
            _matrix03 = newColorMatrix[0][3];
            _matrix04 = newColorMatrix[0][4];
            _matrix10 = newColorMatrix[1][0];
            _matrix11 = newColorMatrix[1][1];
            _matrix12 = newColorMatrix[1][2];
            _matrix13 = newColorMatrix[1][3];
            _matrix14 = newColorMatrix[1][4];
            _matrix20 = newColorMatrix[2][0];
            _matrix21 = newColorMatrix[2][1];
            _matrix22 = newColorMatrix[2][2];
            _matrix23 = newColorMatrix[2][3];
            _matrix24 = newColorMatrix[2][4];
            _matrix30 = newColorMatrix[3][0];
            _matrix31 = newColorMatrix[3][1];
            _matrix32 = newColorMatrix[3][2];
            _matrix33 = newColorMatrix[3][3];
            _matrix34 = newColorMatrix[3][4];
            _matrix40 = newColorMatrix[4][0];
            _matrix41 = newColorMatrix[4][1];
            _matrix42 = newColorMatrix[4][2];
            _matrix43 = newColorMatrix[4][3];
            _matrix44 = newColorMatrix[4][4];
        }

        internal float[][] GetMatrix()
        {
            float[][] array = new float[5][];
            for (int i = 0; i < 5; i++)
            {
                array[i] = new float[5];
            }

            array[0][0] = _matrix00;
            array[0][1] = _matrix01;
            array[0][2] = _matrix02;
            array[0][3] = _matrix03;
            array[0][4] = _matrix04;
            array[1][0] = _matrix10;
            array[1][1] = _matrix11;
            array[1][2] = _matrix12;
            array[1][3] = _matrix13;
            array[1][4] = _matrix14;
            array[2][0] = _matrix20;
            array[2][1] = _matrix21;
            array[2][2] = _matrix22;
            array[2][3] = _matrix23;
            array[2][4] = _matrix24;
            array[3][0] = _matrix30;
            array[3][1] = _matrix31;
            array[3][2] = _matrix32;
            array[3][3] = _matrix33;
            array[3][4] = _matrix34;
            array[4][0] = _matrix40;
            array[4][1] = _matrix41;
            array[4][2] = _matrix42;
            array[4][3] = _matrix43;
            array[4][4] = _matrix44;
            return array;
        }

        internal ref float GetPinnableReference()
        {
            return ref _matrix00;
        }
    }
}