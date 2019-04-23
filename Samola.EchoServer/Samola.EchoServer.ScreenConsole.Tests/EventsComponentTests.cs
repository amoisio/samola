//using EchoServer.ScreenConsole;
//using EchoServer.ScreenConsole.Components.EventsComponent;
//using EchoServer.ScreenConsole.Tests.Fakes;
//using System;
//using Xunit;

//namespace EventsComponentTests
//{
//    public class PrintOffset
//    {
//    //    [Fact]
//    //    public void RenderInstructionsShouldIndicateOffset()
//    //    {
//    //        string title = "A short title";
//    //        EventsComponentFake ec = new EventsComponentFake(title, 1, 5, 40, 40);

//    //        var rit = ec.RenderTitle();

//    //        Assert.Equal(1, rit.X);
//    //        Assert.Equal(5, rit.Y);
//    //    }
//    //}

//    //public class Title
//    //{
//    //    [Fact]
//    //    public void ShouldPrintOutNicelyWhenMoreThanEnoughRoom()
//    //    {
//    //        string title = "A short title";
//    //        EventsComponentFake ec = new EventsComponentFake(title, 0, 0, 40, 40);

//    //        var rit = ec.RenderTitle();

//    //        Assert.Equal(0, rit.X);
//    //        Assert.Equal(0, rit.Y);
//    //        Assert.Equal("=[A short title]========================", rit.RenderString);
//    //    }

//    //    [Fact]
//    //    public void ShouldPrintOutNicelyWhenJustEnoughRoom()
//    //    {
//    //        string title = "A short title";
//    //        EventsComponentFake ec = new EventsComponentFake(title, 0, 0, 17, 40);

//    //        var rit = ec.RenderTitle();

//    //        Assert.Equal(0, rit.X);
//    //        Assert.Equal(0, rit.Y);
//    //        Assert.Equal("=[A short title]=", rit.RenderString);
//    //    }

//    //    [Fact]
//    //    public void ShouldShortenTheTitleWhenNotEnoughRoom()
//    //    {
//    //        string title = "A short title";
//    //        EventsComponentFake ec = new EventsComponentFake(title, 0, 0, 16, 40);

//    //        var rit = ec.RenderTitle();

//    //        Assert.Equal(0, rit.X);
//    //        Assert.Equal(0, rit.Y);
//    //        Assert.Equal("=[A short titl]=", rit.RenderString);
//    //    }
//    //}

//    //public class Headers
//    //{
//    //    [Fact]
//    //    public void ShouldPrintOutBelowTheTitle()
//    //    {
//    //        string title = "A short title";
//    //        EventsComponentFake ec = new EventsComponentFake(title, 0, 0, 40, 40);

//    //        var rit = ec.RenderHeaders();

//    //        Assert.Equal(0, rit.X);
//    //        Assert.Equal(1, rit.Y);
//    //        Assert.Equal("ID  DT                OP   DATA         ", rit.RenderString);
//    //    }

//    //    [Fact]
//    //    public void ShouldSaveSpaceInDataColumn()
//    //    {
//    //        string title = "A short title";
//    //        EventsComponentFake ec = new EventsComponentFake(title, 0, 0, 30, 40);

//    //        var rit = ec.RenderHeaders();

//    //        Assert.Equal(0, rit.X);
//    //        Assert.Equal(1, rit.Y);
//    //        Assert.Equal("ID  DT                OP   DAT", rit.RenderString);
//    //    }
//    //}

//    //public class EventItems
//    //{
//    //    [Fact]
//    //    public void ShouldPrintOutBelowTheHeader()
//    //    {
//    //        string title = "A short title";
//    //        EventsComponentFake ec = new EventsComponentFake(title, 0, 0, 40, 40);
//    //        Event ei = new Event(1, "GET", "First GET");

//    //        var rit = ec.RenderItem(ei, 0);

//    //        Assert.Equal(0, rit.X);
//    //        Assert.Equal(2, rit.Y);
//    //    }

//    //    [Fact]
//    //    public void ShouldIncludeOffsetCalculatingY()
//    //    {
//    //        string title = "A short title";
//    //        EventsComponentFake ec = new EventsComponentFake(title, 0, 0, 40, 40);
//    //        Event ei = new Event(1, "GET", "First GET");

//    //        var rit = ec.RenderItem(ei, 5);

//    //        Assert.Equal(0, rit.X);
//    //        Assert.Equal(7, rit.Y);
//    //    }

//    //    [Fact]
//    //    public void ShouldPrintTheEventItem()
//    //    {
//    //        string title = "A short title";
//    //        EventsComponentFake ec = new EventsComponentFake(title, 0, 0, 40, 40);
//    //        Event ei = new Event(1, "GET", "First GET");

//    //        var rit = ec.RenderItem(ei, 5);

//    //        Assert.Equal(0, rit.X);
//    //        Assert.Equal(7, rit.Y);
//    //        Assert.StartsWith("1   ", rit.RenderString);
//    //        Assert.EndsWith("GET  First GET    ", rit.RenderString);
//    //    }

//    //}
//}
