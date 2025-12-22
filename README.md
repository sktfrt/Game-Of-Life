# Game of Life OOP

Учебный проект по ООП на C# (.NET 9.0) с использованием WinForms.  
Реализована классическая игра "Жизнь" Конвея, а также расширенный режим с добавлением черных клеток.  

---

## Структура проекта

```
GameOfLifeOOP/
│
├─ Program.cs # Точка входа, запускает MainWindow
├─ MainWindow.cs # Основное окно WinForms
├─ MainWindow.Designer.cs 
├─ GameOfLifeOOP.csproj
│
├─ Models/ # Основные сущности проекта
│ ├─ Cell.cs
│ ├─ CellType.cs
│ └─ CellContext.cs
├─ Patterns/
│ ├─ AllPatterns.cs
│ ├─ PatternBase.cs
│ ├─ BlockPattern.cs
│ ├─ BlinkerPattern.cs
│ ├─ GliderPattern.cs
│ ├─ PentadecathlonPattern.cs
│ ├─ BeehivePattern.cs
│ ├─ PatternTerrainDecorator.cs
│ ├─ PatternCell.cs
│ └─ PatternType.cs
│
├─ Strategies/ # Стратегии поведения клеток
│ ├─ ICellLifeStrategy.cs
│ ├─ ClassicEmptyStrategy.cs
│ ├─ ClassicWhiteStrategy.cs
│ ├─ ColonyWhiteStrategy.cs
│ └─ ColonyBlackStrategy.cs
│
├─ Providers/ # Поставщик стратегий
│ ├─ ICellStrategyProvider.cs
│ └─ CellStrategyProvider.cs
│
├─ Factories/ # Фабрики клеток и миров
│ ├─ ICellFactory.cs
│ ├─ SimpleCellFactory.cs
│ ├─ IWorldFactory.cs
│ ├─ ClassicWorldFactory.cs
│ └─ ColoniesWorldFactory.cs
│
├─ Terrain/ # Создание поля клеток
│ └─ Terrain.cs
```

---

## Как запустить

1. Клонируйте проект или скачайте репозиторий.
2. Откройте в VS Code.
3. Убедитесь, что установлен .NET 9.0 SDK.
4. Соберите проект:

```bash
dotnet build
```

5. Запустите проект:
```bash
dotnet run
```

---

## Доступные режимы

### 1. Classic
- Классическая "Жизнь" Конвея
- Только белые живые клетки
- Правила:
  - Клетка рождается, если вокруг ровно 3 живых соседа
  - Клетка остаётся живой при 2–3 соседях
  - Иначе клетка умирает

### 2. Colonies
- Расширенный режим с двумя типами живых клеток: белые и черные
- Клетки могут перекрашиваться, образуя колонии
- Правила:
  - Белая клетка перекрашивается в черную, если черных соседей больше, иначе действует по классическим правилам
  - Черная клетка перекрашивается в белую, если белых соседей больше, иначе живёт при 1–3 соседях
