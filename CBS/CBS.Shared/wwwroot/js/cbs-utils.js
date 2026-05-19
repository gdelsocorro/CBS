// Dismiss loading screen 500ms after Blazor signals it's ready
(function () {
    var dismissed = false;
    function dismiss() {
        if (dismissed) return;
        dismissed = true;
        setTimeout(function () {
            var el = document.getElementById('cbs-loader');
            if (!el) return;
            el.classList.add('hidden');
            setTimeout(function () { el.remove(); }, 400);
        }, 500);
    }
    // Blazor Server fires 'enhancedload' on first navigation; fallback to window load
    document.addEventListener('blazor:connected', dismiss, { once: true });
    window.addEventListener('load', function () { setTimeout(dismiss, 100); });
})();

window.cbsDownloadBytes = function (filename, contentType, base64) {
    const bytes = Uint8Array.from(atob(base64), c => c.charCodeAt(0));
    const blob = new Blob([bytes], { type: contentType });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(url);
};

window.cbsDownloadCsv = function (filename, content) {
    const bom = '﻿'; // UTF-8 BOM for Excel compatibility
    const blob = new Blob([bom + content], { type: 'text/csv;charset=utf-8;' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(url);
};
