mergeInto(LibraryManager.library, {
    CopyToClipboard: function (textToCopy) {
        const toCopy = UTF8ToString(textToCopy);
        if (navigator.clipboard && navigator.clipboard.writeText) {
            navigator.clipboard.writeText(toCopy);
        } else {
            const textArea = document.createElement("textarea");
            textArea.value = toCopy;

            // Avoid scrolling to bottom
            textArea.style.top = "0";
            textArea.style.left = "0";
            textArea.style.position = "fixed";

            document.body.appendChild(textArea);
            textArea.focus();
            textArea.select();

            try {
                const successful = document.execCommand('copy');
            } catch (err) {
            }

            document.body.removeChild(textArea);
        }
    }
});
