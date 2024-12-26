namespace NSubstitute;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var calculator = Substitute.For<ICalculator>(); // モックする ICalculator を取得
        calculator.Add(1, 2).Returns(3); // Add 関数に 1 と 2 が渡された時に 3 を返す
        Assert.Equal(3, calculator.Add(1, 2));
    }

    [Fact]
    public void Test2()
    {
        var calculator = Substitute.For<ICalculator>(); // モックする ICalculator を取得
        calculator.Add(1, 2); // Add 関数の呼び出し
        calculator.Received().Add(1, 2); // Add 関数に引数 1 と 2 の引数の組み合わせで呼ばれたか検証する
        calculator.DidNotReceive().Add(5, 7); // Add 関数に引数 5 と 7 の引数の組み合わせで呼ばれていないか検証する

        // 以下だと 1 と 2 の組み合わせは既に呼ばれているのでテストが失敗する
        // calculator.DidNotReceive().Add(1, 2); 

        // エラーは以下のようになる
        // NSubstitute.Exceptions.ReceivedCallsException: Expected to receive no calls matching:
        // NSubstitute.Exceptions.ReceivedCallsException
        // Expected to receive no calls matching:
        //	Add(1, 2)
        // Actually received 1 matching call:
        //	Add(1, 2)
    }

    [Fact]
    public void Test3()
    {
        var calculator = Substitute.For<ICalculator>(); // モックする ICalculator を取得

        calculator.Mode.Returns("DEC"); // Mode プロパティが "DEC" を返すように設定
        Assert.Equal("DEC", calculator.Mode); // Mode プロパティが "DEC" を返すか検証

        calculator.Mode = "HEX"; // Mode プロパティに "HEX" を設定(上書きすることもできる)
        Assert.Equal("HEX", calculator.Mode); // Mode プロパティが "HEX" を返すか検証
    }

    [Fact]
    public void Test4()
    {
        var calculator = Substitute.For<ICalculator>(); // モックする ICalculator を取得

        calculator.Add(10, -5); // Add 関数の呼び出し
        calculator.Received().Add(10, Arg.Any<int>()); // Add 関数に 10 と任意の整数が渡されたか検証
        calculator.Received().Add(10, Arg.Is<int>(static x => x < 0)); // Add 関数に 10 と負の整数が渡されたか検証
    }
    
    [Fact]
    public void Test5()
    {
        var calculator = Substitute.For<ICalculator>(); // モックする ICalculator を取得

        calculator
           .Add(Arg.Any<int>(), Arg.Any<int>()) // 任意の整数が 2 つ渡された時に
           .Returns(x => (int)x[0] + (int)x[1]); // 2 つの整数の和を返すように設定
        var actual = calculator.Add(5, 10); // 具象クラスの実装はしていないが、Add 関数に 5 と 10 が渡された時に 15 が返る
        Assert.Equal(15, actual); //  5 と 10 を渡すと 15 が返る
    }


    [Fact]
    public void Test6()
    {
        var calculator = Substitute.For<ICalculator>(); // モックする ICalculator を取得

        calculator.Mode.Returns("HEX", "DEC", "BIN"); // Mode プロパティが "HEX"、"DEC"、"BIN" を返すように設定
        Assert.Equal("HEX", calculator.Mode); // Mode プロパティが "HEX" を返すか検証
        Assert.Equal("DEC", calculator.Mode); // Mode プロパティが "DEC" を返すか検証
        Assert.Equal("BIN", calculator.Mode); // Mode プロパティが "BIN" を返すか検証

        Assert.Equal("BIN", calculator.Mode); // 3 回目以降(設定された戻り値の個数より多い呼び出し)は最後の値が返る
    }
    
    [Fact]
    public void Test7()
    {
        var calculator = Substitute.For<ICalculator>(); // モックする ICalculator を取得
        
        var eventWasRaised = false;
        calculator.PoweringUp += (sender, args) => eventWasRaised = true; // PoweringUp イベントが発生した時に eventWasRaised を true にする

        calculator.PoweringUp += Raise.Event(); // PoweringUp イベントを発生させる (sender と eventArgs はデフォルト値)
        Assert.True(eventWasRaised);
    }
    
    public interface ICalculator
    {
        int Add(int a, int b);
        string Mode { get; set; }
        event EventHandler PoweringUp;
    }
}