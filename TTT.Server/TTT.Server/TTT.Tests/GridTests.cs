using FluentAssertions;
using TTT.Server.GameLogic;
using TTT.Shared.Models;

namespace TTT.Tests;

public class Tests
{
    [Test]
    public void WhenTopRowMarkWithX_ThenItShouldBeWinWithTopRow()
    {
        // Arrange
        var grid = new Grid(3);
        
        // Act
        grid.MarkCell(0, MarkType.X);
        grid.MarkCell(1, MarkType.X);
        grid.MarkCell(2, MarkType.X);
        var (isWin, winLineType) = grid.CheckWin();
        
        // Assert
        isWin.Should().BeTrue();
        winLineType.Should().Be(WinLineType.RowTop);
    }

    [Test]
    public void WhenMiddleRowMarkWithX_ThenItShouldBeWinWithMiddleRow()
    {
        // Arrange
        var grid = new Grid(3);
        
        // Act
        grid.MarkCell(3, MarkType.X);
        grid.MarkCell(4, MarkType.X);
        grid.MarkCell(5, MarkType.X);
        var (isWin, winLineType) = grid.CheckWin();
        
        // Assert
        isWin.Should().BeTrue();
        winLineType.Should().Be(WinLineType.RowMiddle);
    }

    [Test]
    public void WhenBottomRowMarkWithX_ThenItShouldBeWinWithBottomRow()
    {
        // Arrange
        var grid = new Grid(3);
        
        // Act
        grid.MarkCell(6, MarkType.X);
        grid.MarkCell(7, MarkType.X);
        grid.MarkCell(8, MarkType.X);
        var (isWin, winLineType) = grid.CheckWin();
        
        // Assert
        isWin.Should().BeTrue();
        winLineType.Should().Be(WinLineType.RowBottom);
    }
    
    [Test]
    public void WhenLeftColumnMarkWithX_ThenItShouldBeWinWithLeftColumn()
    {
        // Arrange
        var grid = new Grid(3);
        
        // Act
        grid.MarkCell(0, 0, MarkType.X);
        grid.MarkCell(0, 1, MarkType.X);
        grid.MarkCell(0, 2, MarkType.X);
        var (isWin, winLineType) = grid.CheckWin();
        
        // Assert
        isWin.Should().BeTrue();
        winLineType.Should().Be(WinLineType.ColLeft);
    }
    
    [Test]
    public void WhenMiddleColumnMarkWithX_ThenItShouldBeWinWithMiddleColumn()
    {
        // Arrange
        var grid = new Grid(3);
        
        // Act
        grid.MarkCell(1, 0, MarkType.X);
        grid.MarkCell(1, 1, MarkType.X);
        grid.MarkCell(1, 2, MarkType.X);
        var (isWin, winLineType) = grid.CheckWin();
        
        // Assert
        isWin.Should().BeTrue();
        winLineType.Should().Be(WinLineType.ColMiddle);
    }
    
    [Test]
    public void WhenRightColumnMarkWithX_ThenItShouldBeWinWithRightColumn()
    {
        // Arrange
        var grid = new Grid(3);
        
        // Act
        grid.MarkCell(2, 0, MarkType.X);
        grid.MarkCell(2, 1, MarkType.X);
        grid.MarkCell(2, 2, MarkType.X);
        var (isWin, winLineType) = grid.CheckWin();
        
        // Assert
        isWin.Should().BeTrue();
        winLineType.Should().Be(WinLineType.ColRight);
    }
    
    [Test]
    public void WhenDiagonalMarkWithX_ThenItShouldBeWinWithDiagonal()
    {
        // Arrange
        var grid = new Grid(3);
        
        // Act
        grid.MarkCell(0, 0, MarkType.X);
        grid.MarkCell(1, 1, MarkType.X);
        grid.MarkCell(2, 2, MarkType.X);
        var (isWin, winLineType) = grid.CheckWin();
        
        // Assert
        isWin.Should().BeTrue();
        winLineType.Should().Be(WinLineType.Diagonal);
    }
    
    [Test]
    public void WhenAntiDiagonalMarkWithX_ThenItShouldBeWinWithAntiDiagonal()
    {
        // Arrange
        var grid = new Grid(3);
        
        // Act
        grid.MarkCell(0, 2, MarkType.X);
        grid.MarkCell(1, 1, MarkType.X);
        grid.MarkCell(2, 0, MarkType.X);
        var (isWin, winLineType) = grid.CheckWin();
        
        // Assert
        isWin.Should().BeTrue();
        winLineType.Should().Be(WinLineType.AntiDiagonal);
    }
    
    [Test]
    public void WhenAllCellsMarkWithDifferentTypes_ThenItShouldBeDraw()
    {
        // Arrange
        var grid = new Grid(3);
        
        // Act
        grid.MarkCell(0, 0, MarkType.X);
        grid.MarkCell(0, 1, MarkType.O);
        grid.MarkCell(0, 2, MarkType.X);
        grid.MarkCell(1, 0, MarkType.O);
        grid.MarkCell(1, 1, MarkType.X);
        grid.MarkCell(1, 2, MarkType.O);
        grid.MarkCell(2, 0, MarkType.O);
        grid.MarkCell(2, 1, MarkType.X);
        grid.MarkCell(2, 2, MarkType.O);
        var isDraw = grid.CheckDraw();
        
        // Assert
        isDraw.Should().BeTrue();
    }
}