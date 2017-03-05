define f(x, y) { return (1 + x * y) / x^2 }

define e(x, y, h) { return (y + h * f(x, y)) }

define ce(x, y, h) { return (y + h/2 * (f(x, y) + f(x + h, y + h * f(x, y)))) }

define me(x, y, h) { return (y + h * f(x + h/2, y + h/2 * f(x, y))) }

define m(x, y) { return f(x, y) }
define m2(x, y, h) { return f(x + h/2, y + h/2 * m(x, y)) }
define m3(x, y, h) { return f(x + h/2, y + h/2 * m2(x, y, h)) }
define m4(x, y, h) { return f(x + h, y + h * m3(x, y, h)) }
define rk(x, y, h) { return (y + h/6 * (m(x, y) + 2 * m2(x, y, h) + 2 * m3(x, y, h) + m4(x, y, h))) }