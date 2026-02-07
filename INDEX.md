# 📚 Навігація по проекту / Project Navigation

## 🎯 З чого почати? / Where to Start?

### Для швидкого огляду (5 хвилин):
1. 📖 **[QUICK_START.md](QUICK_START.md)** - Швидкий старт для розробників

### Для повного розуміння (30 хвилин):
1. 📖 **[README.md](README.md)** - Повна документація архітектури
2. 📊 **[CLASS_DIAGRAM.md](CLASS_DIAGRAM.md)** - Візуальні діаграми
3. 📋 **[SUMMARY.md](SUMMARY.md)** - Підсумок проекту

---

## 📂 Структура документації / Documentation Structure

```
📦 monkey-atlas-privacy/
│
├── 📖 INDEX.md                    ← ВИ ТУТ / YOU ARE HERE
├── 🚀 QUICK_START.md             ← Почніть звідси! / Start here!
├── 📘 README.md                   ← Повна документація / Full docs
├── 📊 CLASS_DIAGRAM.md            ← Діаграми / Diagrams
├── 📋 SUMMARY.md                  ← Підсумок / Summary
│
└── 💻 Scripts/                    ← Код / Code
    ├── Managers/GameManager.cs
    ├── Player/PlayerController.cs
    ├── Enemy/EnemyAI.cs
    ├── UI/UIManager.cs
    └── Data/GameData.cs
```

---

## 🎓 Навчальні треки / Learning Tracks

### 🟢 Початківець / Beginner (2 години)
1. [QUICK_START.md](QUICK_START.md) - Огляд проекту
2. [GameManager.cs](Scripts/Managers/GameManager.cs) - Простий Singleton
3. [PlayerController.cs](Scripts/Player/PlayerController.cs) - Базовий компонент
4. [README.md](README.md) - Секція "Опис класів"

### 🟡 Середній / Intermediate (3 години)
1. [README.md](README.md) - Повна документація
2. [EnemyAI.cs](Scripts/Enemy/EnemyAI.cs) - Finite State Machine
3. [UIManager.cs](Scripts/UI/UIManager.cs) - Управління UI
4. [CLASS_DIAGRAM.md](CLASS_DIAGRAM.md) - Діаграми взаємодії

### 🔴 Просунутий / Advanced (4 години)
1. Всі файли коду - детальне вивчення
2. [CLASS_DIAGRAM.md](CLASS_DIAGRAM.md) - Повний аналіз архітектури
3. [README.md](README.md) - Секція "Розширення системи"
4. [SUMMARY.md](SUMMARY.md) - Архітектурний огляд

---

## 📝 Детальний гід по файлам / Detailed File Guide

### 📖 Документація / Documentation

| Файл | Розмір | Час читання | Призначення |
|------|--------|-------------|-------------|
| **INDEX.md** | 5KB | 3 хв | Навігація (цей файл) |
| **QUICK_START.md** | 7KB | 10 хв | Швидкий старт |
| **README.md** | 18KB | 30 хв | Повна документація |
| **CLASS_DIAGRAM.md** | 28KB | 20 хв | Візуальні діаграми |
| **SUMMARY.md** | 5KB | 5 хв | Підсумок проекту |

### 💻 Код / Code

| Файл | Рядків | Складність | Опис |
|------|--------|-----------|------|
| **GameManager.cs** | 195 | 🟢 Проста | Singleton менеджер гри |
| **PlayerController.cs** | 247 | 🟡 Середня | Управління гравцем |
| **EnemyAI.cs** | 360 | 🔴 Складна | AI з FSM |
| **UIManager.cs** | 320 | 🟡 Середня | Менеджер інтерфейсу |
| **GameData.cs** | 346 | 🟡 Середня | Збереження даних |

---

## 🎯 Сценарії використання / Use Case Scenarios

### 📚 Я хочу навчитися / I want to learn
→ Починайте з [QUICK_START.md](QUICK_START.md)
→ Потім [README.md](README.md)
→ Потім вивчайте код

### 🔍 Я шукаю приклад конкретного патерну / Looking for specific pattern
- **Singleton?** → [GameManager.cs](Scripts/Managers/GameManager.cs)
- **State Machine?** → [EnemyAI.cs](Scripts/Enemy/EnemyAI.cs)
- **Data Persistence?** → [GameData.cs](Scripts/Data/GameData.cs)
- **UI Management?** → [UIManager.cs](Scripts/UI/UIManager.cs)

### 🛠️ Я хочу використати як шаблон / Want to use as template
1. Прочитайте [README.md](README.md) - Секція "Інструкції з налаштування"
2. Скопіюйте папку `Scripts/`
3. Адаптуйте під свій проект
4. Розширюйте через наслідування

### 📊 Мені потрібні діаграми / Need diagrams
→ [CLASS_DIAGRAM.md](CLASS_DIAGRAM.md) - Всі діаграми тут

---

## 🔑 Ключові концепції / Key Concepts

### Що знайдете в цьому проекті:

#### 🎨 Design Patterns
- ✅ Singleton Pattern
- ✅ State Machine (FSM)
- ✅ Component Pattern
- ✅ Observer Pattern

**Де шукати:** [README.md#Використані-патерни](README.md)

#### 🏗️ SOLID Principles
- ✅ Single Responsibility
- ✅ Open/Closed
- ✅ Liskov Substitution
- ✅ Interface Segregation
- ✅ Dependency Inversion

**Де шукати:** [README.md#Принципи-SOLID](README.md)

#### 🎮 Unity Best Practices
- ✅ Component Architecture
- ✅ SerializeField Usage
- ✅ Proper Physics
- ✅ Scene Management
- ✅ Data Persistence

**Де шукати:** [README.md#Найкращі-практики](README.md)

---

## 📊 Карта взаємозв'язків / Relationship Map

```
INDEX.md (ви тут)
    │
    ├─► QUICK_START.md ──► "Швидкий огляд"
    │       │
    │       ├─► GameManager.cs
    │       ├─► PlayerController.cs
    │       └─► EnemyAI.cs
    │
    ├─► README.md ──► "Повна документація"
    │       │
    │       ├─► Опис класів
    │       ├─► Приклади використання
    │       ├─► Інструкції налаштування
    │       └─► Найкращі практики
    │
    ├─► CLASS_DIAGRAM.md ──► "Візуалізація"
    │       │
    │       ├─► Діаграми класів
    │       ├─► State diagrams
    │       └─► Sequence diagrams
    │
    └─► SUMMARY.md ──► "Короткий підсумок"
            │
            ├─► Статистика
            ├─► Досягнення
            └─► Висновки
```

---

## ⏱️ Швидкі посилання / Quick Links

### За часом:
- **5 хвилин:** [QUICK_START.md](QUICK_START.md) → Швидкий огляд
- **15 хвилин:** [SUMMARY.md](SUMMARY.md) → Підсумок проекту
- **30 хвилин:** [README.md](README.md) → Повна документація
- **1 година:** Весь код → Детальне вивчення

### За темою:
- **Архітектура:** [README.md](README.md) + [CLASS_DIAGRAM.md](CLASS_DIAGRAM.md)
- **Патерни:** [README.md#Використані-патерни](README.md)
- **Приклади:** [README.md#Опис-класів](README.md)
- **Налаштування:** [QUICK_START.md#Налаштування](QUICK_START.md)

### За рівнем:
- **Новачок:** [QUICK_START.md](QUICK_START.md) → [GameManager.cs](Scripts/Managers/GameManager.cs)
- **Досвідчений:** [README.md](README.md) → [EnemyAI.cs](Scripts/Enemy/EnemyAI.cs)
- **Експерт:** [CLASS_DIAGRAM.md](CLASS_DIAGRAM.md) → Весь код

---

## 📞 Як отримати допомогу / Getting Help

### Не розумію концепцію:
1. Знайдіть концепцію в [README.md](README.md)
2. Подивіться приклад в коді
3. Перегляньте діаграму в [CLASS_DIAGRAM.md](CLASS_DIAGRAM.md)

### Не знаю, як налаштувати:
→ [QUICK_START.md#Налаштування-в-Unity](QUICK_START.md)

### Хочу розширити систему:
→ [README.md#Розширення-системи](README.md)

---

## ✅ Чеклист вивчення / Learning Checklist

### Рівень 1: Базове розуміння
- [ ] Прочитав INDEX.md (цей файл)
- [ ] Прочитав QUICK_START.md
- [ ] Переглянув структуру проекту
- [ ] Розумію призначення кожного класу

### Рівень 2: Детальне знання
- [ ] Прочитав README.md повністю
- [ ] Вивчив GameManager.cs
- [ ] Вивчив PlayerController.cs
- [ ] Розумію Singleton pattern

### Рівень 3: Поглиблене розуміння
- [ ] Вивчив всі 5 класів
- [ ] Переглянув CLASS_DIAGRAM.md
- [ ] Розумію всі патерни
- [ ] Можу пояснити архітектуру

### Рівень 4: Майстерність
- [ ] Розумію всі взаємозв'язки
- [ ] Можу розширити систему
- [ ] Можу пояснити іншим
- [ ] Готовий використати в проекті

---

## 🎯 Рекомендований шлях навчання / Recommended Learning Path

```
День 1: Огляд
├─► INDEX.md (3 хв)
├─► QUICK_START.md (10 хв)
└─► SUMMARY.md (5 хв)

День 2-3: Базові класи
├─► README.md (30 хв)
├─► GameManager.cs (15 хв)
└─► PlayerController.cs (20 хв)

День 4-5: Складні класи
├─► EnemyAI.cs (25 хв)
├─► UIManager.cs (20 хв)
└─► GameData.cs (20 хв)

День 6-7: Архітектура
├─► CLASS_DIAGRAM.md (20 хв)
└─► Повторне вивчення коду (1 год)
```

---

## 🎓 Після вивчення / After Learning

Ви будете знати:
- ✅ Як структурувати Unity проекти
- ✅ Як застосовувати патерни проектування
- ✅ Як писати чистий, підтримуваний код
- ✅ Як документувати код
- ✅ Як працювати в команді

Ви зможете:
- ✅ Створювати професійні Unity проекти
- ✅ Розширювати існуючі системи
- ✅ Пояснювати архітектурні рішення
- ✅ Навчати інших розробників

---

## 📈 Наступні кроки / Next Steps

1. **Завершили вивчення?**
   - Спробуйте розширити одну з систем
   - Додайте нову функціональність
   - Створіть власний клас

2. **Готові до практики?**
   - Використайте як шаблон для проекту
   - Адаптуйте під свої потреби
   - Поділіться з командою

3. **Хочете більше?**
   - Вивчіть Unity документацію
   - Дослідіть інші патерни
   - Практикуйте на реальних проектах

---

## 🌟 Висновок / Conclusion

Цей проект - це **комплексний навчальний ресурс** та **готовий шаблон** для Unity розробки.

**Приємного навчання та успішної розробки! 🎮**

---

## 📚 Довідка документів / Document Reference

| Документ | Призначення | Коли читати |
|----------|-------------|-------------|
| **INDEX.md** | Навігація | Спочатку |
| **QUICK_START.md** | Швидкий старт | Перший огляд |
| **README.md** | Повна документація | Детальне вивчення |
| **CLASS_DIAGRAM.md** | Візуалізація | Розуміння зв'язків |
| **SUMMARY.md** | Підсумок | Фінальний огляд |

---

**Версія:** 1.0  
**Дата:** 2026-02-07  
**Мова:** Українська / English  
**Статус:** ✅ Завершено / Complete
