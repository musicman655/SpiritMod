using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace SpiritMod.NPCs.Critters.Algae
{
    public class BlueAlgae1 : ModNPC
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Bioluminescent Algae");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults() {
            npc.width = 6;
            npc.height = 6;
            npc.damage = 0;
            npc.defense = 1000;
            npc.lifeMax = 1;
            npc.aiStyle = -1;
            npc.npcSlots = 0;
            npc.noGravity = false;
            npc.alpha = 40;
            npc.behindTiles = true;
            npc.dontCountMe = true;
            npc.dontTakeDamage = true;
        }
        public float num42;
        int num = 0;
        bool collision = false;
        int num1232;
        public override void AI() {
            if(Main.dayTime) {
                num1232++;
                if(num1232 >= Main.rand.Next(100, 700)) {
                    npc.active = false;
                    npc.netUpdate = true;
                }
            }
            num++;
            if(num >= Main.rand.Next(100, 400)) {
                num = 0;
            }
            if(!Main.dayTime) {
                Lighting.AddLight((int)((npc.position.X + (float)(npc.width / 2)) / 16f), (int)((npc.position.Y + (float)(npc.height / 2)) / 16f), 0.148f * 2, 0.191f * 2, .255f * 2);
            }
            npc.spriteDirection = -npc.direction;
            int npcXTile = (int)(npc.Center.X / 16);
            int npcYTile = (int)(npc.Center.Y / 16);
            for(int y = npcYTile; y > Math.Max(0, npcYTile - 100); y--) {
                if(Main.tile[npcXTile, y].liquid != 255) {
                    int liquid = (int)Main.tile[npcXTile, y].liquid;
                    float up = (liquid / 255f) * 16f;
                    npc.position.Y = (y + 1) * 16f - up;
                    break;
                }
            }
            if(!collision) {
                npc.velocity.X = .5f * Main.windSpeed;
            } else {
                npc.velocity.X = -.5f * Main.windSpeed;
            }
            if(npc.collideX || npc.collideY) {
                npc.velocity.X *= -1f;
                if(!collision) {
                    collision = true;
                } else {
                    collision = false;
                }
            }
        }
        public override Color? GetAlpha(Color lightColor) {
            return new Color(176 - (int)(num / 3 * 4), 255 - (int)(num / 3 * 4), 237 - (int)(num / 3 * 4), 255 - num);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor) {
            drawColor = new Color(148 - (int)(num / 3 * 4), 212 - (int)(num / 3 * 4), 255 - (int)(num / 3 * 4), 255 - num);
            var effects = npc.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY - 8), npc.frame,
                             drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, effects, 0);

            return false;
        }
    }
}
