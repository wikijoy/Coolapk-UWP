﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace 酷安_UWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class IndexPage : Page
    {
        MainPage mainPage;
        int page = 1;
        string lastItem;
        public IndexPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            mainPage = e.Parameter as MainPage;
            LoadIndex();
        }

        public async void LoadIndex()
        {
            mainPage.ActiveProgressRing();
            ObservableCollection<Feed> FeedsCollection = new ObservableCollection<Feed>();
            listView.ItemsSource = FeedsCollection;

            JArray Root = await CoolApkSDK.GetIndexList(1, string.Empty);
            lastItem = Root.Last["entityId"].ToString();
            foreach (JObject i in Root)
                FeedsCollection.Add(new Feed(i));
            timer.Interval = new TimeSpan(0, 0, 7);
            timer.Tick += (s, e) =>
            {
                if (flip.SelectedIndex < flip.Items.Count - 1)
                    flip.SelectedIndex++;
                else
                    flip.SelectedIndex = 0;
            };
            timer.Start();
            mainPage.DeactiveProgressRing();
        }

        DispatcherTimer timer = new DispatcherTimer();
        FlipView flip;

        private void FlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (flip is null)
                flip = sender as FlipView;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            timer.Stop();
        }

        /// <summary>
        /// 存放应用支持的表情的数组，表情图片位于/Emoji，文件名里有“(2)”的是同一个表情里的旧表情
        /// </summary>
        public readonly static string[] emojis = new string[] {"(cos滑稽",
"(haha",
"(OK",
"(sofa",
"(what",
"(啊",
"(爱心",
"(暗中观察",
"(鄙视",
"(便便",
"(不高兴",
"(彩虹",
"(茶杯",
"(吃瓜",
"(传统滑稽",
"(大拇指",
"(蛋糕",
"(灯泡",
"(斗鸡眼滑稽",
"(乖",
"(哈哈",
"(汗",
"(呵呵",
"(喝酒",
"(黑头瞪眼",
"(黑头高兴",
"(黑线",
"(嘿嘿嘿",
"(哼",
"(红领巾",
"(呼~",
"(花心",
"(滑稽",
"(滑稽炸",
"(欢呼",
"(稽滑",
"(紧张",
"(惊哭",
"(惊讶",
"(开心",
"(柯基暗中观察",
"(酷",
"(狂汗",
"(困成狗",
"(蜡烛",
"(懒得理",
"(泪",
"(冷",
"(礼物",
"(流汗滑稽",
"(玫瑰",
"(勉强",
"(墨镜滑稽",
"(你懂的",
"(怒",
"(喷",
"(噗",
"(钱",
"(钱币",
"(弱",
"(三道杠",
"(胜利",
"(受虐滑稽",
"(睡觉",
"(酸爽",
"(太开心",
"(太阳",
"(摊手",
"(突然兴奋",
"(吐",
"(吐舌",
"(托腮",
"(挖鼻",
"(微微一笑",
"(委屈",
"(捂嘴笑",
"(犀利",
"(香蕉",
"(小乖",
"(小红脸",
"(小嘴滑稽",
"(笑尿",
"(笑眼",
"(心碎",
"(星星月亮",
"(呀咩爹",
"(药丸",
"(咦",
"(疑问",
"(阴险",
"(音乐",
"(真棒",
"(纸巾",
"[cos滑稽]",
"[doge]",
"[doge并不简单]",
"[doge吃瓜]",
"[doge吃惊]",
"[doge飞吻]",
"[doge告辞]",
"[doge汗]",
"[doge呵斥]",
"[doge互粉]",
"[doge口罩]",
"[doge酷]",
"[doge期待]",
"[doge问号]",
"[doge笑哭]",
"[doge疑问]",
"[doge原谅ta]",
"[doge装酷]",
"[dw哭]",
"[NO]",
"[ok]",
"[py交易]",
"[qqdoge]",
"[t耐克嘴]",
"[w→_→]",
"[wpy交易]",
"[wv5]",
"[ww亲亲]",
"[w爱你]",
"[w奥特曼]",
"[w拜拜]",
"[w悲伤]",
"[w鄙视]",
"[w闭嘴]",
"[w并不简单]",
"[w操练]",
"[w馋嘴]",
"[w吃瓜]",
"[w吃惊]",
"[w打哈欠]",
"[w打脸]",
"[w蛋糕]",
"[w定]",
"[w哆啦a梦吃惊]",
"[w哆啦a梦色]",
"[w哆啦a梦笑]",
"[w肥皂]",
"[w感冒]",
"[w干杯]",
"[w给力]",
"[w鼓掌]",
"[w广而告之]",
"[w跪了]",
"[w哈哈]",
"[w害羞]",
"[w呵呵]",
"[w黑线]",
"[w嘿嘿嘿]",
"[w哼]",
"[w互粉]",
"[w花心]",
"[w急眼]",
"[w囧]",
"[w可爱]",
"[w可怜]",
"[w哭]",
"[w酷]",
"[w酷币]",
"[w困]",
"[w懒得理你]",
"[w累]",
"[w旅行]",
"[w萌]",
"[w男孩儿]",
"[w怒~]",
"[w怒骂]",
"[w女孩儿]",
"[w啤酒鸡腿]",
"[w钱]",
"[w傻眼]",
"[w神马]",
"[w神兽]",
"[w生病]",
"[w失望]",
"[w帅]",
"[w睡觉]",
"[w思考]",
"[w太开心]",
"[w摊手]",
"[w舔]",
"[w调皮]",
"[w偷笑]",
"[w吐]",
"[w兔子]",
"[w挖鼻屎]",
"[w委屈]",
"[w我美吗]",
"[w捂脸哭]",
"[w嘻嘻]",
"[w喜]",
"[w小样儿]",
"[w笑哭]",
"[w新浪]",
"[w熊猫]",
"[w嘘]",
"[w压岁钱]",
"[w疑问]",
"[w阴险]",
"[w右哼哼]",
"[w晕]",
"[w再见]",
"[w织毛衣]",
"[w猪头]",
"[w抓狂]",
"[w左哼哼]",
"[w左哼哼哭]",
"[挨打]",
"[爱你]",
"[爱情]",
"[爱心]",
"[傲慢]",
"[白纹酷币]",
"[白眼]",
"[抱拳]",
"[爆怒]",
"[鄙视]",
"[闭嘴]",
"[便便]",
"[表面开心]",
"[表面哭泣]",
"[不开心]",
"[擦汗]",
"[菜刀]",
"[差劲]",
"[吃瓜]",
"[呲牙]",
"[大兵]",
"[大哭]",
"[蛋糕]",
"[刀]",
"[得意]",
"[凋谢]",
"[斗鸡眼滑稽]",
"[二哈]",
"[二哈盯]",
"[发呆]",
"[发抖]",
"[发怒]",
"[饭]",
"[飞吻]",
"[奋斗]",
"[尴尬]",
"[勾引]",
"[鼓掌]",
"[哈哈]",
"[哈哈哈]",
"[哈欠]",
"[害怕]",
"[害羞]",
"[憨笑]",
"[汗]",
"[呵呵]",
"[喝茶]",
"[喝酒]",
"[黑线]",
"[嘿哈]",
"[嘿嘿]",
"[哼唧]",
"[红药丸]",
"[滑稽]",
"[坏笑]",
"[欢呼]",
"[灰色酷币]",
"[火把]",
"[饥饿]",
"[机智]",
"[假笑]",
"[奸笑]",
"[惊恐]",
"[惊喜]",
"[惊讶]",
"[咖啡]",
"[可爱]",
"[可怜]",
"[抠鼻]",
"[骷髅]",
"[酷]",
"[酷安]",
"[酷安钓鱼]",
"[酷安绿帽]",
"[酷币]",
"[酷币1$]",
"[酷币1]",
"[酷币1€]",
"[酷币1分]",
"[酷币1块]",
"[酷币1毛]",
"[酷币2$]",
"[酷币2]",
"[酷币2€]",
"[酷币2分]",
"[酷币2块]",
"[酷币2毛]",
"[酷币5$]",
"[酷币5]",
"[酷币5€]",
"[酷币5分]",
"[酷币5块]",
"[酷币5毛]",
"[酷币10块]",
"[酷币20块]",
"[酷币50块]",
"[酷币100块]",
"[酷币空]",
"[快哭了]",
"[困]",
"[篮球]",
"[懒得理]",
"[泪奔]",
"[冷汗]",
"[礼物]",
"[流汗]",
"[流汗滑稽]",
"[流泪]",
"[绿帽]",
"[绿色酷币]",
"[绿药丸]",
"[卖萌]",
"[玫瑰]",
"[喵喵]",
"[喵喵鄙视]",
"[喵喵并不简单]",
"[喵喵吃瓜]",
"[喵喵吃惊]",
"[喵喵飞吻]",
"[喵喵告辞]",
"[喵喵汗]",
"[喵喵呵斥]",
"[喵喵互粉]",
"[喵喵口罩]",
"[喵喵酷]",
"[喵喵期待]",
"[喵喵问号]",
"[喵喵笑哭]",
"[喵喵疑问]",
"[喵喵原谅ta]",
"[喵喵再见]",
"[喵喵装酷]",
"[墨镜滑稽]",
"[耐克嘴]",
"[难过]",
"[牛啤]",
"[哦吼吼]",
"[怄火]",
"[喷]",
"[喷血]",
"[啤酒]",
"[瓢虫]",
"[撇嘴]",
"[乒乓]",
"[噗]",
"[强]",
"[敲打]",
"[亲亲]",
"[糗大了]",
"[拳头]",
"[弱]",
"[骚扰]",
"[色]",
"[闪电]",
"[胜利]",
"[示爱]",
"[受虐滑稽]",
"[舒服]",
"[衰]",
"[睡]",
"[太阳]",
"[舔]",
"[挑眉坏笑]",
"[调皮]",
"[跳跳]",
"[偷看]",
"[吐]",
"[吐舌]",
"[托腮]",
"[微微一笑]",
"[微笑]",
"[委屈]",
"[我最美]",
"[握手]",
"[无奈]",
"[无语]",
"[捂脸]",
"[捂嘴笑]",
"[西瓜]",
"[吓]",
"[小纠结]",
"[小嘴滑稽]",
"[笑哭]",
"[笑哭再见]",
"[笑眼]",
"[斜眼笑]",
"[心碎]",
"[新币1分]",
"[新酷币]",
"[新酷币1$]",
"[新酷币1€]",
"[新酷币1块]",
"[新酷币1毛]",
"[新酷币2$]",
"[新酷币2€]",
"[新酷币2分]",
"[新酷币2块]",
"[新酷币2毛]",
"[新酷币5$]",
"[新酷币5€]",
"[新酷币5分]",
"[新酷币5块]",
"[新酷币5毛]",
"[新酷币10块]",
"[新酷币20块]",
"[新酷币50块]",
"[新酷币100块]",
"[嘘]",
"[掩面笑]",
"[耶]",
"[疑问]",
"[阴险]",
"[拥抱]",
"[右哼哼]",
"[月亮]",
"[晕]",
"[再见]",
"[炸弹]",
"[折磨]",
"[咒骂]",
"[皱眉]",
"[猪头]",
"[抓狂]",
"[转圈]",
"[足球]",
"[左哼哼]",
"[Blob滑稽]",
"[Google滑稽]",
"[SegoeUI滑稽]" };

        private void FeedListViewItem_Tapped(object sender, TappedRoutedEventArgs e) => mainPage.Frame.Navigate(typeof(FeedDetailPage), new object[] { ((sender as FrameworkElement).Tag as Feed).GetValue("id"), mainPage, "动态", null });
        private async void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var sv = sender as ScrollViewer;

            if (!e.IsIntermediate)
                if (sv.VerticalOffset == sv.ScrollableHeight)
                {
                    mainPage.ActiveProgressRing();

                    ObservableCollection<Feed> FeedsCollection = listView.ItemsSource as ObservableCollection<Feed>;
                    JArray Root = await CoolApkSDK.GetIndexList(++page, lastItem);
                    if (Root.Count != 0)
                    {
                        lastItem = Root.Last["entityId"].ToString();
                        foreach (JObject i in Root)
                            FeedsCollection.Add(new Feed(i));
                    }
                    else page--;
                    mainPage.DeactiveProgressRing();
                }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button i = sender as Button;
            mainPage.Frame.Navigate(typeof(UserPage), new object[] { i.Tag as string, mainPage });
        }
    }
}
