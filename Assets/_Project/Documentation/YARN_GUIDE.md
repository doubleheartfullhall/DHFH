# Yarn Dialogue Writing Guide for DHFH

## 📝 File Structure

Each story should have **two starting nodes**:
- `Start_EN` for English
- `Start_中文` for Mandarin

## �� Available Commands

### Character Display
```
<<character Mia happy>>
<<character BaoBao excited>>
```

**Expressions**: neutral, happy, thinking, excited, silly, brushing, cooking

### Sound Effects
```
<<sfx brush_teeth>>
<<sfx kitchen_sounds>>
```

### Unlock Stickers
```
<<unlock_sticker good_hygiene>>
```

### Return Home
```
<<return_home>>
```

## ✍️ Writing Tips

1. **Keep sentences short** — Children's attention spans
2. **Use repetition** — "Brush, brush, brush!"
3. **Include choices** — Let children feel agency
4. **Cultural elements** — Weave in Chinese traditions naturally
5. **Positive messaging** — Focus on growth, kindness, curiosity

## 📋 Story Template
```yarn
title: Start_EN
---
<<character Mia happy>>
Hi! I'm Mia. Today we're going to...

-> Choice A
    Response A
    <<jump next_node_EN>>
-> Choice B
    Response B
    <<jump next_node_EN>>
===

title: next_node_EN
---
Continuation of story...
<<unlock_sticker sticker_id>>
<<return_home>>
===
```

## 🌍 Bilingual Best Practices

- Mirror story structure in both languages
- Keep cultural references accessible
- Use pinyin above characters when teaching new words
- Include some code-switching (natural bilingual behavior)

---

**Happy storytelling! 故事愉快！**
