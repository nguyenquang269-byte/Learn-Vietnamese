/**
 * NoiHay Game Engine v2.0
 * Handles Vocabulary, Spelling, and Pronunciation logic.
 */

class GameEngine {
    constructor() {
        this.currentGame = null;
        this.currentStep = 0;
        this.energyStars = 0;
        this.recognition = null;
        this.isRecording = false;
        this.modal = new bootstrap.Modal(document.getElementById('unifiedGameModal'));

        this.initSpeech();
    }

    initSpeech() {
        if ('webkitSpeechRecognition' in window) {
            this.recognition = new webkitSpeechRecognition();
            this.recognition.continuous = false;
            this.recognition.interimResults = false;
            this.recognition.lang = 'vi-VN';

            this.recognition.onresult = (event) => {
                const transcript = event.results[0][0].transcript.toLowerCase();
                this.isRecording = false;
                this.evaluateSpeech(transcript);
            };

            this.recognition.onerror = () => { this.isRecording = false; };
            this.recognition.onend = () => { this.isRecording = false; };
        }
    }

    async launch(type, id, title) {
        this.currentStep = 0;
        this.energyStars = 0;
        document.getElementById('game-title-label').innerText = `🎯 ${title}`;
        this.showLoading();
        this.modal.show();

        if (type === 'lesson') {
            await this.loadLesson(id);
        } else if (type === 'monkey') {
            this.loadMonkeyGame();
        } else if (type === 'dino') {
            this.loadDinoGame();
        }
    }

    showLoading() {
        document.getElementById('game-content-area').innerHTML = `
            <div class="text-center py-5">
                <div class="spinner-border text-primary-dino" style="width: 4rem; height: 4rem;"></div>
                <h2 class="mt-4 font-weight-bold">Bé chờ xíu nhé...</h2>
            </div>
        `;
        document.getElementById('game-footer-controls').innerHTML = '';
    }

    async loadLesson(id) {
        try {
            const response = await fetch(`/api/Game/lesson/${id}`);
            const data = await response.json();
            if (data && data.steps) {
                this.currentGame = { words: data.steps };
                this.render();
            }
        } catch (e) { console.error("Load error:", e); }
    }

    async loadMonkeyGame() {
        try {
            const response = await fetch(`/api/Game/monkey`);
            const data = await response.json();
            if (data && data.steps) {
                this.currentGame = { words: data.steps };
                this.render();
            }
        } catch (e) { this.loadMonkeyGameFallback(); }
    }

    loadMonkeyGameFallback() {
        this.currentGame = {
            words: [
                { text: "Quả x...o", correctValue: "oài", options: ["oài", "oay", "uê"], hint: "Quả ngọt màu vàng", type: 'monkey' },
                { text: "L... lấp", correctValue: "Loáng", options: ["Loáng", "Noáng", "Láng"], hint: "Ánh sáng phản chiếu", type: 'monkey' }
            ]
        };
        this.render();
    }

    loadDinoGame() {
        this.currentGame = {
            words: [
                { text: "Lấp lánh", type: 'pronunciation', audioUrl: '/audio/lap_lanh.mp3' },
                { text: "Xinh xắn", type: 'pronunciation', audioUrl: '/audio/xinh_xan.mp3' },
                { text: "Trong trẻo", type: 'pronunciation', audioUrl: '/audio/trong_treo.mp3' }
            ]
        };
        this.render();
    }

    render() {
        if (!this.currentGame || !this.currentGame.words[this.currentStep]) return;

        const item = this.currentGame.words[this.currentStep];
        const contentArea = document.getElementById('game-content-area');
        const footerArea = document.getElementById('game-footer-controls');
        const total = this.currentGame.words.length;
        const progress = ((this.currentStep + 1) / total) * 100;

        if (item.type === 'monkey') {
            contentArea.innerHTML = `
                <div class="fade-in">
                    <div class="mb-4" style="font-size: 150px;">🐒</div>
                    <h1 class="display-1 font-weight-bold text-dark-blue mb-4">${item.text}</h1>
                    <p class="h4 text-muted mb-5">"${item.hint}"</p>
                    <div class="d-flex justify-content-center gap-4 flex-wrap">
                        ${item.options.map(opt => `
                            <button class="btn btn-3d btn-3d-mint fs-2 px-5" onclick="engine.checkSpelling('${opt}', '${item.correctValue}', this)">${opt}</button>
                        `).join('')}
                    </div>
                </div>
            `;
            footerArea.innerHTML = '';
        } else {
            const imageUrl = item.imageUrl && !item.imageUrl.includes('placeholder') ? item.imageUrl : '/images/v3/pencil_hero.png';
            contentArea.innerHTML = `
                <div class="fade-in">
                    <div class="asset-frame mb-4 float-anim">
                        <img src="${imageUrl}" class="img-fluid shadow-pastel" style="max-height: 280px; width: 350px; object-fit: cover;">
                    </div>
                    <h1 class="display-1 font-weight-bold text-dark-blue mb-2">${item.text}</h1>
                    <p class="h3 text-secondary-dino mb-4">/ ${item.pronunciationGuide || 'Chuẩn xác'} /</p>
                </div>
            `;
            footerArea.innerHTML = `
                <button class="btn btn-3d btn-3d-mint" onclick="engine.playAudio('${item.audioUrl}')"><i class="bi bi-volume-up-fill fs-2"></i> Nghe</button>
                <button id="mic-btn" class="btn btn-3d btn-3d-pink pulse-mic" onclick="engine.toggleMic('${item.text}')"><i class="bi bi-mic-fill fs-2"></i> Nói</button>
                <button class="btn btn-3d btn-3d-warning" onclick="engine.nextStep()">Tiếp tục <i class="bi bi-arrow-right-short fs-2"></i></button>
            `;
        }

        this.updateTrail(progress);
    }

    updateTrail(progress) {
        const runner = document.getElementById('dino-runner');
        if (runner) runner.style.left = `calc(${progress}% - 35px)`;
    }

    checkSpelling(opt, correct, btn) {
        if (opt === correct) {
            btn.classList.add('btn-success');
            this.triggerConfetti();
            setTimeout(() => this.nextStep(), 1200);
        } else {
            btn.classList.add('shake', 'btn-danger');
            setTimeout(() => btn.classList.remove('shake', 'btn-danger'), 600);
        }
    }

    toggleMic(targetText) {
        if (this.isRecording) return;
        this.isRecording = true;
        this.targetWord = targetText.toLowerCase();

        const btn = document.getElementById('mic-btn');
        btn.classList.add('btn-danger');
        btn.innerHTML = '<i class="bi bi-record-circle-fill fs-2"></i> Đang nghe...';

        if (this.recognition) {
            this.recognition.start();
        } else {
            // Fallback for browsers without speech support
            setTimeout(() => this.evaluateSpeech(targetText.toLowerCase()), 1500);
        }
    }

    evaluateSpeech(transcript) {
        const btn = document.getElementById('mic-btn');
        btn.classList.remove('btn-danger');
        btn.innerHTML = '<i class="bi bi-mic-fill fs-2"></i> Nói';

        if (transcript.includes(this.targetWord)) {
            this.triggerConfetti();
            this.energyStars++;
            // Star animation logic could go here
            this.nextStep();
        } else {
            btn.classList.add('shake');
            setTimeout(() => btn.classList.remove('shake'), 600);
        }
    }

    triggerConfetti() {
        confetti({ particleCount: 60, spread: 60, origin: { y: 0.8 } });
    }

    nextStep() {
        if (this.currentStep < this.currentGame.words.length - 1) {
            this.currentStep++;
            this.render();
        } else {
            this.finish();
        }
    }

    finish() {
        this.modal.hide();
        confetti({ particleCount: 300, spread: 120, origin: { y: 0.4 } });
        // Simplified success alert for now
        setTimeout(() => alert("🏅 Bé quá giỏi! Nhận ngay Bằng khen Hiệp sĩ nhé!"), 500);
    }

    playAudio(url) {
        if (!url) return;
        const audio = new Audio(url);
        audio.play().catch(e => console.log("Audio play blocked/not found"));
    }
}

// Global instance
const engine = new GameEngine();
window.launchGame = (type, id, title) => engine.launch(type, id, title);
