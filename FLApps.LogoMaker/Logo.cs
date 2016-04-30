using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FLApps
{
    public class Logo
    {
        private static int MIN_LINES = 1;
        private static int MAX_LINES = 4;

        public static int FINAL_WIDTH = 250;
        public static int FINAL_HEIGHT = 250;

        private static int MIN_RADIUS = 1;
        private static int MAX_RADIUS = 10;

        private static float POINTS_DIST = 1f;

        private List<KeyValuePair<Libs.Vectors.Vector2i, Libs.Vectors.Vector2i>> _lines;
        private List<KeyValuePair<int, int>> _lineRadiuses;

        public Logo(int seed)
        {
            var rand = new Random(seed);

            this._lines = new List<KeyValuePair<Libs.Vectors.Vector2i, Libs.Vectors.Vector2i>>();
            for (int i = 0; i < rand.Next(Logo.MIN_LINES, Logo.MAX_LINES + 1); i++)
                this._lines.Add(new KeyValuePair<Libs.Vectors.Vector2i, Libs.Vectors.Vector2i>(
                    new Libs.Vectors.Vector2i(rand.Next(), rand.Next()),
                    new Libs.Vectors.Vector2i(rand.Next(), rand.Next())
                ));
            this._lineRadiuses = new List<KeyValuePair<int, int>>();
            foreach (var item in this._lines)
            {
                this._lineRadiuses.Add(new KeyValuePair<int, int>(
                    rand.Next(Logo.MIN_RADIUS, Logo.MAX_RADIUS + 1),
                    rand.Next(Logo.MIN_RADIUS, Logo.MAX_RADIUS + 1)));
            }
            if (rand.NextDouble() > .5d) // X Mirror
            {
                int currentLines = this._lines.Count;
                for (int i = 0; i < currentLines; i++)
                {
                    this._lines.Add(new KeyValuePair<Libs.Vectors.Vector2i, Libs.Vectors.Vector2i>(
                        new Libs.Vectors.Vector2i(this._lines[i].Key.X, int.MaxValue - this._lines[i].Key.Y),
                        new Libs.Vectors.Vector2i(this._lines[i].Value.X, int.MaxValue - this._lines[i].Value.Y)
                    ));
                    this._lineRadiuses.Add(this._lineRadiuses[i]);
                }
            }
            if (rand.NextDouble() > .5d) // Y Mirror
            {
                int currentLines = this._lines.Count;
                for (int i = 0; i < currentLines; i++)
                {
                    this._lines.Add(new KeyValuePair<Libs.Vectors.Vector2i, Libs.Vectors.Vector2i>(
                        new Libs.Vectors.Vector2i(int.MaxValue - this._lines[i].Key.X, this._lines[i].Key.Y),
                        new Libs.Vectors.Vector2i(int.MaxValue - this._lines[i].Value.X, this._lines[i].Value.Y)
                    ));
                    this._lineRadiuses.Add(this._lineRadiuses[i]);
                }
            }
        }

        private System.Drawing.Bitmap _render;
        public System.Drawing.Bitmap ToBitmap()
        {
            if (this._render == null)
            {
                //    float width = Logo.FINAL_WIDTH * Logo.RATIO;
                //    float height = Logo.FINAL_HEIGHT * Logo.RATIO;

                this._render = new System.Drawing.Bitmap(Logo.FINAL_WIDTH, Logo.FINAL_HEIGHT);
                var g = System.Drawing.Graphics.FromImage(this._render);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.Clear(System.Drawing.Color.Black);
                int radiusKey = 0;
                foreach (var kvp in this._lines)
                {
                    float x1 = (float)kvp.Key.X / (float)int.MaxValue * (float)Logo.FINAL_WIDTH;
                    float y1 = (float)kvp.Key.Y / (float)int.MaxValue * (float)Logo.FINAL_HEIGHT;
                    float x2 = (float)kvp.Value.X / (float)int.MaxValue * (float)Logo.FINAL_WIDTH;
                    float y2 = (float)kvp.Value.Y / (float)int.MaxValue * (float)Logo.FINAL_HEIGHT;
                    Libs.Vectors.Vector2<float> offset = new Libs.Vectors.Vector2f(x2 - x1, y2 - y1).Normalized().MultipleScalar(Logo.POINTS_DIST);
                    Libs.Vectors.Vector2<float> pos = new Libs.Vectors.Vector2f(x1, y1);
                    int steps = (int)((x2 - x1) / offset.X);
                    KeyValuePair<int,int> lineRadius = this._lineRadiuses[radiusKey];
                    for (int i = 0; i < steps; i++)
                    {
                        float radius = (float)i / (float)steps * (lineRadius.Value - lineRadius.Key) + lineRadius.Key;
                        g.DrawEllipse(
                            System.Drawing.Pens.White,
                            pos.X - radius / 2f, pos.Y - radius / 2f,
                            radius, radius);

                        pos = pos.Add(offset);
                    }

                    radiusKey++;
                }
            }

            return new System.Drawing.Bitmap(this._render);
        }
    }
}
