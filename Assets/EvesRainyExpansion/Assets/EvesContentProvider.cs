using System.Collections;
using RoR2;
using RoR2.ContentManagement;
using RoR2.ExpansionManagement;
using UnityEngine;

namespace EvesRainyExpansion
{
    public class EvesContentProvider : IContentPackProvider
    {
        private static ItemDef _itemDef;
        private static AssetBundle _assetBundle;
        
        public string identifier => EvesRainyExpansionMain.GUID;
        public static ReadOnlyContentPack ReadOnlyContentPack => new(EvesRainyExpansionContentPack);
        internal static ContentPack EvesRainyExpansionContentPack { get; } = new();

        internal EvesContentProvider()
        {
            ContentManager.collectContentPackProviders += AddSelf;
        }

        public IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
        {
            AssetBundleCreateRequest asyncOperation = AssetBundle.LoadFromFileAsync(EvesRainyExpansionMain.AssetBundleDir);
            while (!asyncOperation.isDone)
            {
                args.ReportProgress(asyncOperation.progress);
                yield return null;
            }
            
            _assetBundle = asyncOperation.assetBundle;
            _itemDef = _assetBundle.LoadAsset<ItemDef>("TestItem");
            ExpansionDef expansionDef = _assetBundle.LoadAsset<ExpansionDef>("TestExpansion");

            EvesRainyExpansionContentPack.itemDefs.Add(new[] { _itemDef });
            EvesRainyExpansionContentPack.expansionDefs.Add(new[] { expansionDef });
        }

        public IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args)
        {
            ContentPack.Copy(EvesRainyExpansionContentPack, args.output);
            args.ReportProgress(1f);
            yield break;
        }

        public IEnumerator FinalizeAsync(FinalizeAsyncArgs args)
        {
            R2API.RecalculateStatsAPI.GetStatCoefficients += (body, statArgs) =>
            {
                if (!body.inventory)
                    return;

                int count = body.inventory.GetItemCountEffective(_itemDef);
                statArgs.attackSpeedMultAdd += 0.1f * count;
                statArgs.critDamageMultAdd += 0.1f * count;
                statArgs.damageMultAdd += 0.1f * count;
                statArgs.healthMultAdd += 0.1f * count;
                statArgs.jumpPowerMultAdd += 0.1f * count;
                statArgs.moveSpeedMultAdd += 0.1f * count;
                statArgs.regenMultAdd += 0.1f * count;
                statArgs.shieldMultAdd += 0.1f * count;
            };
            
            args.ReportProgress(1f);
            yield break;
        }

        private void AddSelf(ContentManager.AddContentPackProviderDelegate addContentPackProvider)
        {
            addContentPackProvider(this);
        }
    }
}