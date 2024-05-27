﻿var formatoFecha = new Intl.DateTimeFormat('es-ES', {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
    second: '2-digit',
    hour12: false
});

function formatearFecha(fecha) {
    return formatoFecha.format(new Date(fecha));
}