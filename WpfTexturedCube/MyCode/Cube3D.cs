using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace WpfTexturedCube.MyCode
{
    class Cube3D : ModelVisual3D
    {
                public Cube3D()
        {
            DrawCube(_size, _pos);
        }

        private double _size = 0.5;
        public double Size
        {
            get => _size;
            set
            {
                _size = value;
                DrawCube(_size, _pos);
            }
        }

        private Point3D _pos;
        public Point3D Position
        {
            get => _pos;
            set
            {
                _pos = value;
                DrawCube(_size, _pos);
            }
        }
        // Материалы граней
        public ImageBrush myImageBrush = new ImageBrush(new BitmapImage(new Uri("IVT-20.jpg", UriKind.Relative)));
        public ImageBrush _front = new ImageBrush(new BitmapImage(new Uri("r1.jpg", UriKind.Relative)));
        public ImageBrush _top = new ImageBrush(new BitmapImage(new Uri("r2.jpg", UriKind.Relative)));
        public ImageBrush _left = new ImageBrush(new BitmapImage(new Uri("r3.jpg", UriKind.Relative)));
        public ImageBrush _right = new ImageBrush(new BitmapImage(new Uri("r4.jpg", UriKind.Relative)));
        public ImageBrush _back = new ImageBrush(new BitmapImage(new Uri("r5.jpg", UriKind.Relative)));
        public ImageBrush _bottom = new ImageBrush(new BitmapImage(new Uri("r6.jpg", UriKind.Relative)));

        private static GeometryModel3D AddFace(
            Point3D point1,
            Point3D point2,
            Point3D point3,
            Point3D point4,
            Material material)
        {
            GeometryModel3D geometryModel3D = new GeometryModel3D()
            {
                Geometry = new MeshGeometry3D()
                {
                    Positions = new Point3DCollection()
                    {
                        point1,
                        point2,
                        point3,
                        point3,
                        point4,
                        point1
                    },
                    TextureCoordinates = new PointCollection()
                    {
                        new Point(0, 1),
                        new Point(0, 0),
                        new Point(1, 0),
                        new Point(1, 0),
                        new Point(1, 1),
                        new Point(0, 1)
                    }
                }
            };
            geometryModel3D.Material = material;
            return geometryModel3D;
        }
        private void DrawCube(
                    double size,
                    Point3D pos
        )
        {
            // Отсчёт точек от левого нижнего угла грани.


            // Размерности граней симметричны в обе стороны в абсолютных величинах.
            double absX = size / 2;
            double absY = size / 2;
            double absZ = size / 2;


            Point3D front_left_bottom = new Point3D(-absX + pos.X, -absY + pos.Y, absZ + pos.Z);
            Point3D front_right_bottom = new Point3D(absX + pos.X, -absY + pos.Y, absZ + pos.Z);
            Point3D front_right_top = new Point3D(absX + pos.X, absY + pos.Y, absZ + pos.Z);
            Point3D front_left_top = new Point3D(-absX + pos.X, absY + pos.Y, absZ + pos.Z);
            Point3D backside_right_top = new Point3D(absX + pos.X, absY + pos.Y, -absZ + pos.Z);
            Point3D backside_left_top = new Point3D(-absX + pos.X, absY + pos.Y, -absZ + pos.Z);
            Point3D backside_left_bottom = new Point3D(-absX + pos.X, -absY + pos.Y, -absZ + pos.Z);
            Point3D backside_right_bottom = new Point3D(absX + pos.X, -absY + pos.Y, -absZ + pos.Z);


            Model3DGroup m3dg = new Model3DGroup();

            // 1 Передняя
            DiffuseMaterial material = new DiffuseMaterial();
            material.Brush = _front;
            GeometryModel3D faceFront = AddFace(
                    front_left_bottom,
                    front_right_bottom,
                    front_right_top,
                    front_left_top,
                    material);

            m3dg.Children.Add(faceFront);

            // 2 Верхняя

            material = new DiffuseMaterial();
            material.Brush = _top;
            GeometryModel3D faceTop =
                AddFace(
                    front_left_top,
                    front_right_top,
                    backside_right_top,
                    backside_left_top,
                    material);
            m3dg.Children.Add(faceTop);

            // 3 Левая
            material = new DiffuseMaterial();
            material.Brush = _left;
            GeometryModel3D faceLeft =
                AddFace(
                    backside_left_bottom,
                    front_left_bottom,
                    front_left_top,
                    backside_left_top,
                    material);
            m3dg.Children.Add(faceLeft);

            // 4 Правая
            material = new DiffuseMaterial();
            material.Brush = _right;
            GeometryModel3D faceRight =
                AddFace(
                    front_right_bottom,
                    backside_right_bottom,
                    backside_right_top,
                    front_right_top,
                    material);
            m3dg.Children.Add(faceRight);

            // 5 Нижняя
            material = new DiffuseMaterial();
            material.Brush = _bottom;
            GeometryModel3D faceBottom =
                AddFace(
                    backside_left_bottom,
                    backside_right_bottom,
                    front_right_bottom,
                    front_left_bottom,
                    material);
            m3dg.Children.Add(faceBottom);

            // 6 Задняя
            material = new DiffuseMaterial();
            material.Brush = _back;
            GeometryModel3D faceBack =
                AddFace(
                    backside_right_bottom,
                    backside_left_bottom,
                    backside_left_top,
                    backside_right_top,
                    material);
            m3dg.Children.Add(faceBack);
            Content = m3dg;
        }

    }
}
