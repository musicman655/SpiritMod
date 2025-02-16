using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameInput;

namespace SpiritMod.Items.Sets.Vulture_Matriarch
{
	public class Vulture_Matriarch_Mask_Visuals : ModPlayer
	{
		public static bool maskEquipped = false;
		public override void ResetEffects()
		{
			maskEquipped = false;
		}
		public override void ModifyDrawLayers(List<PlayerLayer> layers)
		{
			int head = layers.FindIndex(l => l == PlayerLayer.Head);
			if (head < 0)
				return;

			layers.Insert(head, new PlayerLayer(mod.Name, "Head",
				delegate(PlayerDrawInfo drawInfo)
			{
				if (drawInfo.shadow != 0f)
				{
					return;
				}
				Player drawPlayer = drawInfo.drawPlayer;
				
				if ((maskEquipped && drawPlayer.armor[10].type == 0) || drawPlayer.armor[10].type == mod.ItemType("Vulture_Matriarch_Mask"))
				{			
					Mod mod = ModLoader.GetMod("SpiritMod");
					Vector2 Position = drawPlayer.position;
					DrawData drawData = new DrawData();
					SpriteEffects spriteEffects;
					SpriteEffects effect;
					if ((double) drawPlayer.gravDir == 1.0)
					{
						if (drawPlayer.direction == 1)
						{
							spriteEffects = SpriteEffects.None;
							effect = SpriteEffects.None;
						}
						else
						{
							spriteEffects = SpriteEffects.FlipHorizontally;
							effect = SpriteEffects.FlipHorizontally;
						}
					}
					else
					{
						if (drawPlayer.direction == 1)
						{
							spriteEffects = SpriteEffects.FlipVertically;
							effect = SpriteEffects.FlipVertically;
						}
						else
						{
							spriteEffects = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
							effect = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
						}
					}
					Microsoft.Xna.Framework.Color color12 = drawPlayer.GetImmuneAlphaPure(Lighting.GetColor((int) ((double) Position.X + (double) drawPlayer.width * 0.5) / 16, (int) ((double) Position.Y + (double) drawPlayer.height * 0.5) / 16, Microsoft.Xna.Framework.Color.White), drawPlayer.shadow);
					Texture2D helmTexture = mod.GetTexture("Items/Sets/Vulture_Matriarch/Vulture_Matriarch_Mask_Head_2");

					Vector2 helmPos = new Vector2((int)(drawInfo.position.X - Main.screenPosition.X) + ((drawInfo.drawPlayer.width - drawInfo.drawPlayer.bodyFrame.Width) / 2), (int)(drawInfo.position.Y - Main.screenPosition.Y) + drawInfo.drawPlayer.height - drawInfo.drawPlayer.bodyFrame.Height - 2) + drawInfo.drawPlayer.headPosition + drawInfo.headOrigin;
					DrawData drawData3 = new DrawData(helmTexture, helmPos, new Microsoft.Xna.Framework.Rectangle?(drawPlayer.bodyFrame), color12, drawInfo.drawPlayer.headRotation, drawInfo.headOrigin, 1f, spriteEffects, 0);
					Main.playerDrawData.Add(drawData3);
					
				}
				
			}));
		}
	}
}