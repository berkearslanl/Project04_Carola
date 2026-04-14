(function () {
    "use strict";

    // Mesaj geçmişi (oturum bazlı)
    const chatHistory = [];

    function appendMessage(role, text) {
        const messagesDiv = document.getElementById("chat-messages");
        const wrapper = document.createElement("div");
        wrapper.classList.add("chat-msg", role === "user" ? "chat-msg--user" : "chat-msg--bot");

        const bubble = document.createElement("div");
        bubble.classList.add("chat-bubble");
        bubble.textContent = text;

        wrapper.appendChild(bubble);
        messagesDiv.appendChild(wrapper);
        messagesDiv.scrollTop = messagesDiv.scrollHeight;
    }

    function appendTyping() {
        const messagesDiv = document.getElementById("chat-messages");
        const wrapper = document.createElement("div");
        wrapper.classList.add("chat-msg", "chat-msg--bot");
        wrapper.id = "typing-indicator";

        const bubble = document.createElement("div");
        bubble.classList.add("chat-bubble", "chat-typing");
        bubble.innerHTML = "<span></span><span></span><span></span>";

        wrapper.appendChild(bubble);
        messagesDiv.appendChild(wrapper);
        messagesDiv.scrollTop = messagesDiv.scrollHeight;
    }

    function removeTyping() {
        const el = document.getElementById("typing-indicator");
        if (el) el.remove();
    }

    async function sendMessage() {
        const input = document.getElementById("chat-input");
        const message = input.value.trim();
        if (!message) return;

        input.value = "";
        appendMessage("user", message);
        appendTyping();

        try {
            const response = await fetch("/api/Chat/send", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ message })
            });

            const data = await response.json();
            removeTyping();

            if (data.reply) {
                appendMessage("bot", data.reply);
            } else {
                appendMessage("bot", "Üzgünüm, şu an cevap veremiyorum. Lütfen tekrar deneyin.");
            }
        } catch (e) {
            removeTyping();
            appendMessage("bot", "Bağlantı hatası oluştu. Lütfen tekrar deneyin.");
        }
    }

    document.addEventListener("DOMContentLoaded", function () {
        const toggle = document.getElementById("chat-toggle-btn");
        const closeBtn = document.getElementById("chat-close-btn");
        const chatBox = document.getElementById("chat-box");
        const sendBtn = document.getElementById("chat-send-btn");
        const input = document.getElementById("chat-input");

        if (!toggle) return;

        // İlk açılışta karşılama mesajı
        let greeted = false;

        toggle.addEventListener("click", function () {
            chatBox.classList.toggle("chat-box--open");
            toggle.classList.toggle("chat-toggle-btn--open");
            if (!greeted) {
                greeted = true;
                setTimeout(() => {
                    appendMessage("bot", "Merhaba! Ben Carola Asistan. Araç kiralama konusunda size nasıl yardımcı olabilirim?");
                }, 300);
            }
        });

        closeBtn.addEventListener("click", function () {
            chatBox.classList.remove("chat-box--open");
            toggle.classList.remove("chat-toggle-btn--open");
        });

        sendBtn.addEventListener("click", sendMessage);

        input.addEventListener("keydown", function (e) {
            if (e.key === "Enter" && !e.shiftKey) {
                e.preventDefault();
                sendMessage();
            }
        });
    });
})();
