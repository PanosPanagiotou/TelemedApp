window.applyMask = (el, mask) => {
    const format = (value) => {
        let i = 0;
        return mask.replace(/9/g, _ => value[i++] || "");
    };

    el.addEventListener("input", () => {
        const digits = el.value.replace(/\D/g, "");
        el.value = format(digits);
    });
};