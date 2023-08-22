# 스네이크 게임


## 스네이크 게임 요구 사항
---
- 스네이크는 자신이 바라보는 방향으로 게임이 끝날때까지 움직입니다.
- 유저는 방향키를 입력해서 스네이크의 방향을 제어할 수 있습니다.
- 맵의 랜덤한 위치에 음식을 생성합니다.
- 스네이크가 음식을 먹으면 스네이크의 길이가 한 개씩 늘어납니다.
- 스네이크가 맵의 범위를 벗어나면 게임 오버됩니다.
- 스네이크가 자신의 몸에 부딪히면 게임 오버됩니다.

<br/>
<br/>

## 게임 플레이 영상
---

1) 게임 플레이 화면

<img src="https://github.com/yarogono/Console_Snake_Game/assets/70641418/38149833-cfe8-4432-b70e-07135f48c55f">  

<br/>

2) 게임 오버

<img src="https://github.com/yarogono/Console_Snake_Game/assets/70641418/912daa99-00c5-472c-9372-8713cbc15136">  
  
<img src="https://github.com/yarogono/Console_Snake_Game/assets/70641418/5cb6b2cc-d0d8-43db-8669-512366dbdbf8">

Snake가 Map을 벗어나면 게임 오버됩니다.

Snake의 머리가 몸통에 부딪히면 게임이 종료됩니다.
초록색이 Snake(뱀)입니다.
빨간색은 Food(음식)입니다.

게임 오버가 되면 바로 게임이 종료 되고 'Game Over' 문구가 뜹니다.

<br/>
<br/>


## 프로젝트 설계
---

![image](https://github.com/yarogono/Console_Snake_Game/assets/70641418/4366a180-e599-4498-8a57-6b6a08505170)


- Program 클래스
  - 게임의 시작 부분인 Main 메서드가 있는 클래스입니다.
  - GameManager 객체를 생성해서 StartGame() 메서드를 호출합니다.
- GameManager 클래스
  - 전반적으로 게임을 관리하는 클래스입니다.
  - Snake, Food 객체를 초기화 값을 결정하는 코드가 담겨 있습니다.
  - 유저가 입력하는 키의 값을 체크하고 해당 키에 맞게 Snake의 행동 관련 메서드를 호출합니다.
  - 게임이 끝나는 조건을 체크하고 끝나면 'Game Over' 문구를 콘솔에 출력합니다.
  - Snake가 Food를 먹었는지 체크하고 먹었으면 Snake 클래스의 EatFood() 메서드를 호출합니다.
  - Map 객체를 사용해서 Map 렌더링 메서드를 호출해 콘솔에 출력합니다.
- Food 클래스
  - Food를 생성하는 메서드와 Snake가 음식을 먹으면 리스폰 하는 메서드가 있습니다.
  - Food 객체의 좌표 상태를 관리합니다.
- Snake 클래스
  - Snake의 Head를 좌표에 맞게 움직이는 메서드와 몸통(Body)를 움직이는 메서드가 있습니다.
  - 유저가 방향키를 입력하면 해당 방향키에 맞게 방향을 수정합니다.
  - Snake의 방향과 좌표 값을 상태로 관리합니다.
- Map 클래스
  - Render() 메서드를 통해 Map Size에 맞게 출력합니다.
- Pos 클래스
  - 좌표(Position)에 관한 클래스입니다.

<br/>
<br/>

## 구현하면서 고민한 것들
---

코드를 작성하면서 이 부분은 꼭 집고 넘어가자는 것들이 있었습니다.
그리고 이런 부분들은 다른 게임 개발에도 충분히 사용될 만한 것들이라고 생각했습니다.

제가 고민했던 것들을 한 번 정리하면서 회고해 봤습니다.

<br/>

<details>
<summary>1) 유저에게 방향키 값을 입력 받는 부분에 대한 고민</summary>
<div markdown="1">       

C#에서 콘솔에 입력되는 키보드 값을 입력 받는 코드는 아래와 같습니다.

```csharp
while (true)
{
	// ... 게임 로직 ...
    
	ConsoleKeyInfo consoleKey = Console.ReadKey();
    
    // ... 게임 로직 ...
}
```

하지만 게임이 실행되는 반복문에 위와 같이 코드를 작성하면 문제가 있습니다.
키보드 값이 입력 받을 때까지 계속 기다리기 때문에 게임의 흐름이 계속 중단 된다는 것입니다.

그래서 방법을 찾다가 방향키 값을 입력받는 스레드를 만들어 처리하도록 구현해봤습니다.

```csharp
Task.Factory.StartNew(() =>
{
	while(true)
	{
		consoleKey = Console.ReadKey();


		switch (consoleKey.Key)
		{
			case ConsoleKey.LeftArrow:
				snake.SwitchDirection(Snake.Dir.Left);
				break;
			case ConsoleKey.RightArrow:
				snake.SwitchDirection(Snake.Dir.Right);
				break;
			case ConsoleKey.UpArrow:
				snake.SwitchDirection(Snake.Dir.Up);
				break;
			case ConsoleKey.DownArrow:
				snake.SwitchDirection(Snake.Dir.Down);
				break;
			case ConsoleKey.Q:
				return;
		}
	}
});
```

Task 클래스를 사용해 스레드풀로부터 스레드를 가져와 비동기 작업을 실행합니다.
Task.Factory.StartNew()는 실행하고자 하는 메서드에 대한 델리게이트를 지정할 수 있습니다.
제가 구현한 방법은 메서드를 작성하지 않고 람다식(Arrow function)을 사용해서 구현했습니다.

하지만 구현하면서 느낀것은 자그마한 콘솔 게임에서
'과연 스레드가 하나 더 필요할까?🤔'
라는 생각을 했습니다.

그래서 좀 더 찾아보니까 아주 쉬운 방법이 있었습니다.

```csharp
if (Console.KeyAvailable)
{
	ConsoleKeyInfo consoleKey = Console.ReadKey();

	switch (consoleKey.Key)
	{
		case ConsoleKey.LeftArrow:
			_snake.SwitchDirection(Snake.Dir.Left);
			break;
		case ConsoleKey.RightArrow:
			_snake.SwitchDirection(Snake.Dir.Right);
			break;
		case ConsoleKey.UpArrow:
			_snake.SwitchDirection(Snake.Dir.Up);
			break;
		case ConsoleKey.DownArrow:
			_snake.SwitchDirection(Snake.Dir.Down);
			break;
		case ConsoleKey.Q:
			return;
	}
}
```
Console.KeyAvailable 한 줄로 모든 것이 해결되었습니다.

입력 스트림에서 키 누름을 사용할 수 있는지 여부를 나타내는 값을 가져오는 데 사용된다고 합니다.

위 코드를 한 줄 작성하면 기존에 있었던 유저의 키보드 입력을 기다려서 발생하는 문제는 해결됩니다.

</div>
</details>

<br/>

<details>
<summary>2) Snake의 이동 관련 로직</summary>
<div markdown="1">       

Snake의 이동 관련 로직은 2D 게임의 로직과 비슷한 부분이 있었습니다.
케릭터(Snake)는 방향(Direction)과 좌표(x, y) 값을 상태로 관리해야 합니다.

그래서 방향은 Enum을 사용해서 관리하고 좌표는 Pos 클래스를 사용했습니다.

```csharp
// 방향(Direction) 관련 enum
public enum Dir
{
	Up = 0,
	Left = 1,
	Down = 2,
	Right = 3
}

// 좌표 혹은 위치(Position) 관련 클래스
public class Pos
{
	public Pos(int y, int x) { Y = y; X = x; }
	public int Y;
	public int X;
}
```
Snake는 최초 객체 생성 시 위쪽(Up)으로 이동합니다.
방향키를 입력하면 해당 방향키에 해당하는 상태를 Snake 객체가 갖게 됩니다.

그러면 키를 입력하지 않았을 때는 마지막에 입력한 방향으로 계속 움직이게 되는 것입니다.

움직이는 것에 대한 처리는 상당히 쉽게 해결했습니다.

```csharp
public void MoveSnake()
{
	_headPosY = _positions[0].Y;
	_headPosX = _positions[0].X;

	// Switch문을 사용해서 Snake의 방향에 맞게 로직 처리
	switch (_dir)
	{
		case (int)Dir.Up:
			_positions[0].Y--;
			MoveSnakeBody();
			break;
		case (int)Dir.Down:
			_positions[0].Y++;
			MoveSnakeBody();
			break;
		case (int)Dir.Left:
			_positions[0].X--;
			MoveSnakeBody();
			break;
		case (int)Dir.Right:
			_positions[0].X++;
			MoveSnakeBody();
			break;
	}
}
```
플레이어가 입력한 방향으로 좌표 값 x, y가 줄어들거나 늘어나게 됩니다.

이 기능은 최초에 Snake의 길이가 하나일 때는 상당히 쉽게 구현했지만,
음식을 먹었을 때 길이가 늘어나면 어떻게 구현할지에 대해서 고민을 많이 했습니다.

</div>
</details>

<br/>

<details>
<summary>3) Snake가 음식을 먹었을 때 처리 로직</summary>
<div markdown="1">       

첫 번째로 Snake가 List로 좌표 값을 가지고 있도록 구현했습니다.

```csharp
public List<Pos> Positions { get { return _positions; } }
private List<Pos> _positions = new List<Pos>();
```
음식이 배치된 좌표로 Snake의 머리가 이동하면 음식을 먹는 처리가 됩니다.
(Snake의 좌표와 음식의 좌표가 같은지 확인하는 조건문으로 처리)

음식을 먹는 처리는 상당히 간단합니다.

```csharp
public void EatFood(Pos foodPos)
{
	Pos snakeBodyPos = new Pos(foodPos.Y, foodPos.X);
	_positions.Add(snakeBodyPos);
}
```
음식의 좌표 Pos를 Snake의 좌표 List에 추가하는 것입니다.

이렇게 추가된 노드는 아래의 로직을 통해서 Snake가 움직이는 좌표에 맞게 제대로 처리가 됩니다.

```charp
private void MoveSnakeBody()
{
	if (_positions.Count > 1)
	{
		_positions[_positions.Count - 1].Y = _headPosY;
		_positions[_positions.Count - 1].X = _headPosX;
		_positions.Insert(1, _positions[_positions.Count - 1]);
		_positions.RemoveAt(_positions.Count - 1);
	}
}
```

위 로직은 Snake의 길이가 1개 이상이면 실행되는 로직입니다.
게임이 진행되고 Snake가 계속 움직이는 동안 일정한 주기로 계속 실행됩니다.

만약에 Snake의 Position 갯수가 2개가 되면 1개는 머리가 되고, 나머지 1개는 몸통이 됩니다.

위 로직을 순서에 맞게 정리하면

1. Snake의 position Count가 1개 이상이면 조건문 실행
2. Snake의 position List의 마지막 노드의 좌표 값에 머리가 있었던 좌표 값을 넣습니다.
3. 마지막 노드를 머리 다음 인덱스인 '1'에 새로 추가합니다.
4. 마지막 노드를 삭제합니다.

이렇게 구현한 이유는 몸통을 움직일 때 모든 몸통을 반복으로 좌표를 변경하는게 아니라,
마지막 노드(꼬리)만 머리 다음 인덱스로 옮기면 되기 때문에 이렇게 구현했습니다.

콘솔에서는 이렇게 구현해도 플레이어가 보기에는 크게 문제가 없습니다.
그리고 전부 이동하는 것보다는 좋은 성능을 보이게 되어 이렇게 구현하게 되었습니다.

![image](https://github.com/yarogono/Console_Snake_Game/assets/70641418/453a1d56-facc-4f33-b86b-bf28f084ed55)

글 작성을 하다 보니까 좀 더 간단하게 해결하는 방법을 알게 되었습니다.

```csharp
if (_positions.Count > 1)
{
	Pos snakeNewBody = new Pos(_headPosY, _headPosX);
	_positions.Insert(1, snakeNewBody);
	_positions.RemoveAt(_positions.Count - 1);
}
```

아예 새로운 Pos 객체를 생성해서 head의 좌표값을 입력하고 1 번 인덱스는 추가 하는 것입니다.
그리고 마지막 노드를 삭제합니다.

작동은 동일하지만 훨씬 더 깔끔한 코드가 되었습니다.

</div>
</details>

<br/>

<details>
<summary>4) 게임 Tick을 사용해 프레임 설정</summary>
<div markdown="1">       

``csharp
while (true)
{
	// ... 게임 로직 ...
	Thread.Sleep(100);
}

```

위와 같이 게임의 실행 속도를 Thread를 Sleep에서 컨트롤했습니다.

스레드 하나를 일정 시간동안 멈추(대기)는 것은 문제가 있을 것 같아서 수정 해야 겠다고 생각했습니다.

- Thread.Sleep()은 특정 시간 동안 블록되기 때문에 코드의 흐름을 예측하기 어렵워지고 유지보수가 힘들어집니다.
- 멀티 스레드 환경이 되면 교착상태와 관련된 위험이 있습니다.
  - 락의 사용 없이 'Thread.Sleep()'을 사용하면 교착상태(deadlock)와 같은 문제가 발생할 수 있습니다.
- Thread.Sleep()을 사용하면 그만큼 대기를 하는 것이기 때문에 시스템 자원을 낭비하게 됩니다.

결론은 Thread를 Lock이나 스케쥴링을 통해 관리하는게 아닌 Sleep과 같은 방식으로 직접 통제를 하면 문제를 야기 시킬 수 있습니다.

그래서 도입한 다른 방법은

```csharp
private int _sumTick = 0;

private const int WAIT_TICK = 1000 / 10;
private const int MOVE_TICK = 1000;

public void StartGame()
{
	_map = new Map();
	_snake = new Snake();
	_food = new Food();
	
	_map.Initialize(_mapSize);
	_snake.Initialize(10, 12);
	_food.CreateFood(5, 5);

	Console.CursorVisible = false;
	int lastTick = 0;
	while (true)
	{
    	// 프레임 관리 로직
		#region 프레임 관리
		// 만약에 경과한 시간이 1/30초보다 작다면
		int currentTick = System.Environment.TickCount;
		if (currentTick - lastTick < WAIT_TICK)
			continue;

		int deltaTick = currentTick - lastTick;
		lastTick = currentTick;
		#endregion

		// ... 게임 로직 ...

		_sumTick += deltaTick;
		if (_sumTick >= MOVE_TICK)
		{
			_snake.MoveSnake();
			_map.Render(_snake.Positions, _food.Position);
		}
	}
}
```

TickCount를 사용해서 프레임을 직접 관리하는 코드를 추가합니다.
이렇게 틱으로 프레임을 관리하는 코드를 Snake가 움직이는 로직과 Map을 렌더링 하는 부분에만 추가합니다.

Snake가 움직이는 로직, Map을 렌더링 하는 로직을 프레임에 맞게 수정할 수 있습니다.
그래서 원하는 프레임에 맞춰서 게임을 실행하도록 할 수 있습니다.

</div>
</details>
