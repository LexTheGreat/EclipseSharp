/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI.Net - Texus's Graphical User Interface for .Net languages
// Copyright (C) 2013-2014 Bruno Van de Velde (vdv_b@tgui.eu)
//
// This software is provided 'as-is', without any express or implied warranty.
// In no event will the authors be held liable for any damages arising from the use of this software.
//
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it freely,
// subject to the following restrictions:
//
// 1. The origin of this software must not be misrepresented;
//    you must not claim that you wrote the original software.
//    If you use this software in a product, an acknowledgment
//    in the product documentation would be appreciated but is not required.
//
// 2. Altered source versions must be plainly marked as such,
//    and must not be misrepresented as being the original software.
//
// 3. This notice may not be removed or altered from any source distribution.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace TGUI
{
    public class SpriteSheet : Picture
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor, only intended for internal use
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal SpriteSheet ()
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Copy constructor
        /// </summary>
        ///
        /// <param name="copy">Instance to copy</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public SpriteSheet (SpriteSheet copy) : base(copy)
        {
            m_Cells       = copy.m_Cells;
            m_VisibleCell = copy.m_VisibleCell;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the picture by loading an image from a file.
        /// </summary>
        ///
        /// <param name="filename">The absolute or relative filename of the image that should be loaded.</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public SpriteSheet (string filename) : base(filename)
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Size of the sprite sheet
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override Vector2f Size
        {
            get
            {
                return m_Size;
            }
            set
            {
                m_Size = value;

                // Make sure the sprite has the correct size
                m_Sprite.sprite.Scale = new Vector2f((m_Size.X * m_Cells.X) / m_Sprite.texture.texture.Size.X, (m_Size.Y * m_Cells.Y) / m_Sprite.texture.texture.Size.Y);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The number of cells (Vector2u(rows, columns)) in the image
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Vector2u Cells
        {
            get
            {
                return m_Cells;
            }
            set
            {
                m_Cells = value;

                // You can't have 0 columns
                if (m_Cells.X == 0)
                    m_Cells.X = 1;

                // You can't have 0 rows
                if (m_Cells.Y == 0)
                    m_Cells.Y = 1;

                // Make the correct part of the image visible
                m_Sprite.sprite.TextureRect = new IntRect((int)((m_VisibleCell.X-1) * m_Sprite.texture.texture.Size.X / m_Cells.X),
                                                          (int)((m_VisibleCell.Y-1) * m_Sprite.texture.texture.Size.Y / m_Cells.Y),
                                                          (int)(m_Sprite.texture.texture.Size.X / m_Cells.X),
                                                          (int)(m_Sprite.texture.texture.Size.Y / m_Cells.Y));

                // Make sure the sprite has the correct size
                m_Sprite.sprite.Scale = new Vector2f((m_Size.X * m_Cells.X) / m_Sprite.texture.texture.Size.X, (m_Size.Y * m_Cells.Y) / m_Sprite.texture.texture.Size.Y);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Which cell (Vector2u(row, column)) should be displayed?
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Vector2u VisibleCell
        {
            get
            {
                return m_Cells;
            }
            set
            {
                m_VisibleCell = value;

                // You can't make a column visible that doesn't exist
                if (m_VisibleCell.X > m_Cells.X)
                    m_VisibleCell.X = m_Cells.X;
                else if (m_VisibleCell.X == 0)
                    m_VisibleCell.X = 1;

                // You can't make a row visible that doesn't exist
                if (m_VisibleCell.Y > m_Cells.Y)
                    m_VisibleCell.Y = m_Cells.Y;
                else if (m_VisibleCell.Y == 0)
                    m_VisibleCell.Y = 1;

                // Make the correct part of the image visible
                m_Sprite.sprite.TextureRect = new IntRect((int)((m_VisibleCell.X-1) * m_Sprite.texture.texture.Size.X / m_Cells.X),
                                                          (int)((m_VisibleCell.Y-1) * m_Sprite.texture.texture.Size.Y / m_Cells.Y),
                                                          (int)(m_Sprite.texture.texture.Size.X / m_Cells.X),
                                                          (int)(m_Sprite.texture.texture.Size.Y / m_Cells.Y));
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private Vector2u m_Cells = new Vector2u(1, 1);
        private Vector2u m_VisibleCell = new Vector2u(0, 0);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
