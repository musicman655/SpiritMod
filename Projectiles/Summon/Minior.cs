using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Projectiles.Summon
{
    public class Minior : ModProjectile
    {

        int timer;

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Mini Meteor");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 2;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            Main.projFrames[projectile.type] = 1;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.Spazmamini);
            projectile.width = 30;
            projectile.height = 30;
            projectile.minion = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.scale = .9f;
            projectile.netImportant = true;
            aiType = ProjectileID.Spazmamini;
            projectile.alpha = 0;
            projectile.penetrate = -10;
            projectile.timeLeft = 18000;
            projectile.minionSlots = 1;
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            if(projectile.penetrate == 0)
                projectile.Kill();

            return false;
        }

        public override void AI() {
            Player player = Main.player[projectile.owner];
            bool flag64 = projectile.type == ModContent.ProjectileType<Minior>();
            MyPlayer modPlayer = player.GetSpiritPlayer();
            if(flag64) {
                if(player.dead)
                    modPlayer.minior = false;

                if(modPlayer.minior)
                    projectile.timeLeft = 2;

            }
        }

        public override void Kill(int timeLeft) {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 50);
            for(int i = 0; i < 5; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 187);
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for(int k = 0; k < projectile.oldPos.Length; k++) {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return false;
        }

    }
}