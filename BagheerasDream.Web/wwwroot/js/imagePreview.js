window.imagePreview = {
    createFromInput: (inputId) => {
        const input = document.getElementById(inputId);

        if (!input || !input.files || input.files.length === 0) {
            return null;
        }

        return URL.createObjectURL(input.files[0]);
    },

    revoke: (url) => {
        if (url) {
            URL.revokeObjectURL(url);
        }
    }
};