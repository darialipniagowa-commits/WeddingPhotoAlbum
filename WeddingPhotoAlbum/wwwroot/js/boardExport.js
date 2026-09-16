window.weddingBoardExport = {
    createCanvas: async function () {
        if (typeof window.html2canvas !== "function") {
            throw new Error(
                "html2canvas is not loaded. Check index.html."
            );
        }

        const board =
            document.getElementById("photo-quest-export");

        if (!board) {
            throw new Error(
                "The photo quest export element was not found."
            );
        }

        if (document.fonts && document.fonts.ready) {
            await document.fonts.ready;
        }

        const images =
            Array.from(board.querySelectorAll("img"));

        await Promise.all(
            images.map(image => {
                if (image.complete) {
                    return Promise.resolve();
                }

                return new Promise(resolve => {
                    image.addEventListener(
                        "load",
                        resolve,
                        { once: true }
                    );

                    image.addEventListener(
                        "error",
                        resolve,
                        { once: true }
                    );
                });
            })
        );

        return await window.html2canvas(board, {
            backgroundColor: "#f4eddf",
            scale: 2,
            useCORS: true,
            allowTaint: true,
            logging: false,
            imageTimeout: 15000,
            removeContainer: true,

            ignoreElements: element =>
                element.hasAttribute(
                    "data-html2canvas-ignore"
                )
        });
    },

    canvasToBlob: function (canvas) {
        return new Promise((resolve, reject) => {
            canvas.toBlob(
                blob => {
                    if (blob) {
                        resolve(blob);
                        return;
                    }

                    reject(
                        new Error(
                            "The browser could not create the PNG."
                        )
                    );
                },
                "image/png",
                1
            );
        });
    },

    downloadBlob: function (blob, fileName) {
        const url =
            URL.createObjectURL(blob);

        const link =
            document.createElement("a");

        link.href = url;
        link.download = fileName;
        link.style.display = "none";

        document.body.appendChild(link);

        link.click();
        link.remove();

        window.setTimeout(() => {
            URL.revokeObjectURL(url);
        }, 1000);
    },

    downloadPng: async function () {
        const canvas =
            await window.weddingBoardExport.createCanvas();

        const blob =
            await window.weddingBoardExport.canvasToBlob(
                canvas
            );

        window.weddingBoardExport.downloadBlob(
            blob,
            "daria-janis-photo-quest.png"
        );

        return "downloaded";
    },

    sharePng: async function () {
        const canvas =
            await window.weddingBoardExport.createCanvas();

        const blob =
            await window.weddingBoardExport.canvasToBlob(
                canvas
            );

        const file = new File(
            [blob],
            "daria-janis-photo-quest.png",
            {
                type: "image/png",
                lastModified: Date.now()
            }
        );

        const shareData = {
            title:
                "Дарья и Янис — Свадебный фотоквест",

            text:
                "Наш свадебный фотоквест — 19.09.2026",

            files: [file]
        };

        const supportsFileSharing =
            typeof navigator.share === "function" &&
            typeof navigator.canShare === "function" &&
            navigator.canShare({
                files: [file]
            });

        if (supportsFileSharing) {
            await navigator.share(shareData);

            return "shared";
        }

        window.weddingBoardExport.downloadBlob(
            blob,
            "daria-janis-photo-quest.png"
        );

        return "downloaded";
    }
};