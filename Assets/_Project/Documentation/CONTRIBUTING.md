# Contributing to DHFH

## 🎨 Adding New Stories

1. Create `.yarn` file in `Assets/_Project/YarnDialogue/`
2. Follow naming convention: `character_theme.yarn`
3. Include both `Start_EN` and `Start_中文` nodes
4. Test in Unity before committing

## 🖼️ Adding Art Assets

1. Export at **2x resolution** for Retina displays
2. Use **PNG with transparency** for characters
3. Keep **warm color palette** (see UI_STYLE.md)
4. Place in appropriate `Art/` subfolder

## 🔊 Adding Audio

1. **SFX**: WAV format, 44.1kHz, mono
2. **Music**: MP3 format, 128kbps
3. Keep files under 1MB when possible
4. Name descriptively: `action_context.wav`

## 📝 Code Style

- Use **meaningful variable names**
- Add **XML comments** for public methods
- Follow **Unity naming conventions**
- Keep scripts under 300 lines

## 🧪 Testing Checklist

- [ ] Compiles without errors
- [ ] Works on both EN and 中文
- [ ] Touch targets are 60x60px minimum
- [ ] No console errors during play
- [ ] Tested on mobile device (if possible)

---

Thank you for contributing! 感谢您的贡献！
