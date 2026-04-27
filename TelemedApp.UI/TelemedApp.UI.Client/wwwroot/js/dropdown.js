window.dropdown = {
    autoAlign: (triggerId, menuId) => {
        const trigger = document.getElementById(triggerId);
        const menu = document.getElementById(menuId);

        if (!trigger || !menu) return;

        const t = trigger.getBoundingClientRect();
        const m = menu.getBoundingClientRect();
        const vw = window.innerWidth;
        const vh = window.innerHeight;

        let vertical = "down";
        let horizontal = "right";

        if (t.bottom + m.height > vh) vertical = "up";
        if (t.left + m.width > vw) horizontal = "left";

        return { vertical, horizontal };
    }
};