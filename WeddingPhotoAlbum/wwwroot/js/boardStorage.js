window.weddingBoardStorage = {
    databaseName: "WeddingPhotoQuestDatabase",
    storeName: "boards",
    databaseVersion: 1,

    openDatabase: function () {
        return new Promise((resolve, reject) => {
            const request = indexedDB.open(
                this.databaseName,
                this.databaseVersion
            );

            request.onupgradeneeded = event => {
                const database = event.target.result;

                if (!database.objectStoreNames.contains(this.storeName)) {
                    database.createObjectStore(
                        this.storeName,
                        { keyPath: "boardId" }
                    );
                }
            };

            request.onsuccess = event => {
                resolve(event.target.result);
            };

            request.onerror = event => {
                reject(event.target.error);
            };
        });
    },

    save: async function (boardId, progress) {
        const database = await this.openDatabase();

        return new Promise((resolve, reject) => {
            const transaction = database.transaction(
                this.storeName,
                "readwrite"
            );

            const store = transaction.objectStore(this.storeName);

            store.put({
                boardId: boardId,
                value: progress,
                savedAt: new Date().toISOString()
            });

            transaction.oncomplete = () => {
                database.close();
                resolve(true);
            };

            transaction.onerror = event => {
                database.close();
                reject(event.target.error);
            };
        });
    },

    load: async function (boardId) {
        const database = await this.openDatabase();

        return new Promise((resolve, reject) => {
            const transaction = database.transaction(
                this.storeName,
                "readonly"
            );

            const store = transaction.objectStore(this.storeName);
            const request = store.get(boardId);

            request.onsuccess = () => {
                database.close();

                if (!request.result) {
                    resolve(null);
                    return;
                }

                resolve(request.result.value);
            };

            request.onerror = event => {
                database.close();
                reject(event.target.error);
            };
        });
    },

    remove: async function (boardId) {
        const database = await this.openDatabase();

        return new Promise((resolve, reject) => {
            const transaction = database.transaction(
                this.storeName,
                "readwrite"
            );

            transaction.objectStore(this.storeName).delete(boardId);

            transaction.oncomplete = () => {
                database.close();
                resolve(true);
            };

            transaction.onerror = event => {
                database.close();
                reject(event.target.error);
            };
        });
    }
};