using SpiritMod.Items.Consumable;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SpiritMod.Tiles.Furniture
{
    public class ReachCrate_Tile : ModTile
    {
        public override void SetDefaults() {
            Main.tileTable[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.addTile(Type);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY) {
            Terraria.Item.NewItem(i * 16, j * 16, 64, 32, ModContent.ItemType<ReachCrate>());
        }
    }
}