(function(a) {
    var c = a.document;
    a.doesEnvSupportEditor = (function() {
        if (!c.createElement) {
            return
        }
        var e = c.createElement("canvas");
        if (!e) {
            return
        }
        if (!e.getContext) {
            if (typeof G_vmlCanvasManager != "undefined" && G_vmlCanvasManager.initElement) {
                G_vmlCanvasManager.initElement(e);
                if (!e.getContext) {
                    return
                }
            }
        }
        var d = e.getContext("2d");
        if (!d) {
            return
        }
        return true
    })();
    if (!a.doesEnvSupportEditor) {
        var b = c.getElementById("loading-indicator");
        if (b) {
            b.innerHTML = "This browser can not display editor. Sorry.";
            b.style.left = "30%"
        }
    }
})(this);
if (APE !== undefined) {
    throw Error("APE is already defined.")
}
var APE = {extend: function(a, e, b) {
        if (arguments.length === 0) {
            return
        }
        var d = arguments.callee, c;
        d.prototype = e.prototype;
        a.prototype = c = new d;
        if (typeof b == "object") {
            APE.mixin(c, b)
        }
        c.constructor = a;
        return a
    },mixin: function(e, d) {
        var c = ["toString", "valueOf"], f, b = 0, a;
        for (f in d) {
            if (d.hasOwnProperty(f)) {
                e[f] = d[f]
            }
        }
        for (; b < c.length; b++) {
            a = c[b];
            if (d.hasOwnProperty(a)) {
                e[a] = d[a]
            }
        }
        return e
    },toString: function() {
        return "[APE JavaScript Library]"
    },getByNode: function(a) {
        var c = a.id, b;
        if (!c) {
            if (!APE.getByNode._i) {
                APE.getByNode._i = 0
            }
            b = APE.getFunctionName(this);
            if (!b) {
                b = "APE"
            }
            c = a.id = b + "_" + (APE.getByNode._i++)
        }
        if (!this.hasOwnProperty("instances")) {
            this.instances = {}
        }
        return this.instances[c] || (this.instances[c] = APE.newApply(this, arguments))
    },getFunctionName: function(a) {
        if (typeof a.name == "string") {
            return a.name
        }
        var b = Function.prototype.toString.call(a).match(/\s([a-z]+)\(/i);
        return b && b[1] || ""
    },getById: function(a) {
        if (!this.hasOwnProperty("instances")) {
            this.instances = {}
        }
        return this.instances[a] || (this.instances[a] = APE.newApply(this, arguments))
    },createFactory: function(b, c) {
        var a = {}, e = a.instances = {};
        if (c) {
            b.prototype = c
        }
        a.getById = d;
        return a;
        function d(f) {
            return e[f] || (e[f] = APE.newApply(b, arguments))
        }
    },newApply: (function() {
        function b() {
        }
        return a;
        function a(e, c) {
            var d;
            b.prototype = e.prototype;
            b.prototype.constructor = e;
            d = new b;
            e.apply(d, c);
            return d
        }
    })(),deferError: function(a) {
        throw a
    }};
(function() {
    APE.namespace = a;
    function a(g) {
        var h = g.split("."), e = window, k = Object.prototype.hasOwnProperty, j = e.qualifiedName, f = 0, c = h.length, d;
        for (; f < c; f++) {
            d = h[f];
            if (!k.call(e, d)) {
                e[d] = new b((j || "APE") + "." + d)
            }
            e = e[d]
        }
        return e
    }
    b.prototype.toString = function() {
        return "[" + this.qualifiedName + "]"
    };
    function b(c) {
        this.qualifiedName = c
    }
})();
(function() {
    var b = Object.prototype, a = b.hasOwnProperty;
    if (typeof window != "undefined" && a && !a.call(window, "Object")) {
        Object.prototype.hasOwnProperty = function(c) {
            if (this === window) {
                return (c in this) && (b[c] !== this[c])
            }
            return a.call(this, c)
        }
    }
})();
APE.EventPublisher = function(b, a) {
    this.src = b;
    this._callStack = [];
    this.type = a
};
APE.EventPublisher.prototype = {add: function(b, a) {
        this._callStack.push([b, a || this.src]);
        return this
    },addBefore: function(b, a) {
        return APE.EventPublisher.add(this, "beforeFire", b, a)
    },addAfter: function(b, a) {
        return APE.EventPublisher.add(this, "afterFire", b, a)
    },getEvent: function(a) {
        return APE.EventPublisher.get(this, a)
    },remove: function(c, b) {
        var f = this._callStack, d = 0, a, e;
        if (!b) {
            b = this.src
        }
        for (a = f.length; d < a; d++) {
            e = f[d];
            if (e[0] === c && e[1] === b) {
                return f.splice(d, 1)
            }
        }
        return null
    },removeBefore: function(b, a) {
        return this.getEvent("beforeFire").remove(b, a)
    },removeAfter: function(b, a) {
        return this.getEvent("afterFire").remove(b, a)
    },fire: function(a) {
        return APE.EventPublisher.fire(this)(a)
    },toString: function() {
        return "APE.EventPublisher: {src=" + this.src + ", type=" + this.type + ", length=" + this._callStack.length + "}"
    }};
APE.EventPublisher.add = function(d, c, b, a) {
    return APE.EventPublisher.get(d, c).add(b, a)
};
APE.EventPublisher.fire = function(b) {
    return a;
    function a(k) {
        var f = false, h = 0, c, j = b._callStack, d;
        if (typeof b.beforeFire == "function") {
            try {
                if (b.beforeFire(k) == false) {
                    f = true
                }
            } catch (g) {
                APE.deferError(g)
            }
        }
        for (c = j.length; h < c; h++) {
            d = j[h];
            try {
                if (d[0].call(d[1], k || window.event) == false) {
                    f = true
                }
            } catch (g) {
                APE.deferError(g)
            }
        }
        if (typeof b.afterFire == "function") {
            if (b.afterFire(k) == false) {
                f = true
            }
        }
        return !f
    }
};
APE.EventPublisher.get = function(f, e) {
    var d = this.Registry.hasOwnProperty(e) && this.Registry[e] || (this.Registry[e] = []), b = 0, a = d.length, c;
    for (; b < a; b++) {
        if (d[b].src === f) {
            return d[b]
        }
    }
    c = new APE.EventPublisher(f, e);
    if (f[e]) {
        c.add(f[e], f)
    }
    f[e] = this.fire(c);
    d[d.length] = c;
    return c
};
APE.EventPublisher.Registry = {};
APE.EventPublisher.cleanUp = function() {
    var c, e, d, b, a;
    for (c in this.Registry) {
        e = this.Registry[c];
        for (b = 0, a = e.length; b < a; b++) {
            d = e[b];
            d.src[d.type] = null
        }
    }
};
if (window.CollectGarbage) {
    APE.EventPublisher.get(window, "onunload").addAfter(APE.EventPublisher.cleanUp, APE.EventPublisher)
}
APE.namespace("APE.dom");
(function() {
    var c = APE.dom, d = document.documentElement, b = "textContent", a = document.defaultView;
    c.IS_COMPUTED_STYLE = (typeof a != "undefined" && "getComputedStyle" in a);
    c.textContent = b in d ? b : "innerText"
})();
(function() {
    APE.mixin(APE.dom, {getScrollOffsets: a,getViewportDimensions: c});
    var b = "documentElement", e = document[b], d = e && e.clientWidth === 0;
    e = null;
    function a(i) {
        i = i || window;
        var h, j = i.document, g = j[b];
        if ("pageXOffset" in i) {
            h = function() {
                return {left: i.pageXOffset,top: i.pageYOffset}
            }
        } else {
            if (d) {
                g = j.body
            }
            h = function() {
                return {left: g.scrollLeft,top: g.scrollTop}
            }
        }
        j = null;
        this.getScrollOffsets = h;
        return h()
    }
    function c(j) {
        j = j || window;
        var g = j.document, k = g, i = "client", l, h;
        if (typeof k.clientWidth == "number") {
        } else {
            if (d || f(j)) {
                g = k.body
            } else {
                if (k[b].clientHeight > 0) {
                    g = k[b]
                } else {
                    if (typeof innerHeight == "number") {
                        g = j;
                        i = "inner"
                    }
                }
            }
        }
        l = i + "Width";
        h = i + "Height";
        return (this.getViewportDimensions = function() {
            return {width: g[l],height: g[h]}
        })();
        function f(n) {
            var o = n.document, p = o.createElement("div");
            p.style.height = "2500px";
            o.body.insertBefore(p, o.body.firstChild);
            var m = o[b].clientHeight > 2400;
            o.body.removeChild(p);
            return m
        }
    }
})();
(function() {
    APE.mixin(APE.dom, {getOffsetCoords: z,isAboveElement: d,isBelowElement: u,isInsideElement: j});
    var F = this.document, y, E = F.documentElement, B = Math.round, w = Math.max, o = E && E.clientWidth === 0, p = "clientTop" in E, n = /^h/.test(E.tagName) ? "table" : "TABLE", k = "currentStyle" in E, r, c, C, t, q, m, e, h, a, l, D, f = F.defaultView && typeof F.defaultView.getComputedStyle != "undefined", g = "getBoundingClientRect", A = "relative", x = "borderTopWidth", b = "borderLeftWidth", i = /^(?:r|a)/, s = /^(?:a|f)/;
    function z(H, X, ad) {
        if (H === null) {
            return {x: 0,y: 0}
        }
        var aj = H.ownerDocument, ah = aj.documentElement, U = aj.body;
        if (!X) {
            X = aj
        }
        if (!ad) {
            ad = {x: 0,y: 0}
        }
        if (H === X) {
            ad.x = ad.y = 0;
            return ad
        }
        if (g in H) {
            var af = o ? U : ah, S = H[g](), R = S.left + w(ah.scrollLeft, U.scrollLeft), P = S.top + w(ah.scrollTop, U.scrollTop), ac, Z = af.clientTop, I = af.clientLeft;
            if (p) {
                R -= I;
                P -= Z
            }
            if (X !== aj) {
                S = z(X, null);
                R -= S.x;
                P -= S.y;
                if (o && X === U && p) {
                    R -= I;
                    P -= Z
                }
            }
            if (o && k && X != aj && X !== U) {
                ac = U.currentStyle;
                R += parseFloat(ac.marginLeft) || 0 + parseFloat(ac.left) || 0;
                P += parseFloat(ac.marginTop) || 0 + parseFloat(ac.top) || 0
            }
            ad.x = R;
            ad.y = P;
            return ad
        } else {
            if (f) {
                if (!y) {
                    v()
                }
                var N = H.offsetLeft, ae = H.offsetTop, aa = aj.defaultView, M = aa.getComputedStyle(H, "");
                if (M.position == "fixed") {
                    ad.x = N + ah.scrollLeft;
                    ad.y = ae + ah.scrollTop;
                    return ad
                }
                var T = aa.getComputedStyle(U, ""), V = !i.test(T.position), L = H, O = H.parentNode, G = H.offsetParent;
                for (; O && O !== X; O = O.parentNode) {
                    if (O !== U && O !== ah) {
                        N -= O.scrollLeft;
                        ae -= O.scrollTop
                    }
                    if (O === G) {
                        if (O === U && V) {
                        } else {
                            if (!r && !(O.tagName === n && q)) {
                                var K = aa.getComputedStyle(O, "");
                                N += parseFloat(K[b]) || 0;
                                ae += parseFloat(K[x]) || 0
                            }
                            if (O !== U) {
                                N += G.offsetLeft;
                                ae += G.offsetTop;
                                L = G;
                                G = O.offsetParent
                            }
                        }
                    }
                }
                var Q = 0, ab = 0, ai, W, ag = X === aj || X === ah, Y, J;
                if (L != aj) {
                    J = aa.getComputedStyle(L, "").position;
                    ai = s.test(J);
                    W = ai || i.test(J)
                }
                if ((L === H && H.offsetParent === U && !c && X !== U && !(V && t)) || (c && L === H && !W) || !V && W && e && ag) {
                    ab += parseFloat(T.marginTop) || 0;
                    Q += parseFloat(T.marginLeft) || 0
                }
                if (X === U) {
                    Y = aa.getComputedStyle(ah, "");
                    if ((!V && ((a && !ai) || (l && ai))) || V && h) {
                        ab -= parseFloat(Y.paddingTop) || 0;
                        Q -= parseFloat(Y.paddingLeft) || 0
                    }
                    if (D) {
                        if (!W || W && !V) {
                            ab -= parseFloat(Y.marginTop) || 0
                        }
                        Q -= parseFloat(Y.marginLeft) || 0
                    }
                }
                if (V) {
                    if (m || (!ai && !r && ag)) {
                        ab += parseFloat(T[x]);
                        Q += parseFloat(T[b])
                    }
                } else {
                    if (t) {
                        if (ag) {
                            if (!C) {
                                ab += parseFloat(T.top) || 0;
                                Q += parseFloat(T.left) || 0;
                                if (ai && r) {
                                    ab += parseFloat(T[x]);
                                    Q += parseFloat(T[b])
                                }
                            }
                            if (X === aj && !V && !a) {
                                if (!Y) {
                                    Y = aa.getComputedStyle(ah, "")
                                }
                                ab += parseFloat(Y.paddingTop) || 0;
                                Q += parseFloat(Y.paddingLeft) || 0
                            }
                        } else {
                            if (C) {
                                ab -= parseFloat(T.top);
                                Q -= parseFloat(T.left)
                            }
                        }
                        if (c && (!W || X === U)) {
                            ab -= parseFloat(T.marginTop) || 0;
                            Q -= parseFloat(T.marginLeft) || 0
                        }
                    }
                }
                ad.x = B(N + Q);
                ad.y = B(ae + ab);
                return ad
            }
        }
    }
    function v() {
        y = true;
        var P = F.body;
        if (!P) {
            return
        }
        var G = "marginTop", O = "position", T = "padding", M = "static", L = "border", W = P.style, J = W.cssText, S = "1px solid transparent", Q = "0", N = "1px", H = "offsetTop", I = E.style, V = I.cssText, R = F.createElement("div"), K = R.style, U = F.createElement(n);
        W[T] = W[G] = W.top = Q;
        I.position = M;
        W[L] = S;
        K.margin = Q;
        K[O] = M;
        R = P.insertBefore(R, P.firstChild);
        r = (R[H] === 1);
        W[L] = Q;
        U.innerHTML = "<tbody><tr><td>x</td></tr></tbody>";
        U.style[L] = "7px solid";
        U.cellSpacing = U.cellPadding = Q;
        P.insertBefore(U, P.firstChild);
        q = U.getElementsByTagName("td")[0].offsetLeft === 7;
        P.removeChild(U);
        W[G] = N;
        W[O] = A;
        c = (R[H] === 1);
        t = P[H] === 0;
        W[G] = Q;
        W.top = N;
        C = R[H] === 1;
        W.top = Q;
        W[G] = N;
        W[O] = K[O] = A;
        e = R[H] === 0;
        K[O] = "absolute";
        W[O] = M;
        if (R.offsetParent === P) {
            W[L] = S;
            K.top = "2px";
            m = R[H] === 1;
            W[L] = Q;
            K[O] = A;
            I[T] = N;
            W[G] = Q;
            h = R[H] === 3;
            W[O] = A;
            a = R[H] === 3;
            K[O] = "absolute";
            l = R[H] === 3;
            I[T] = Q;
            I[G] = N;
            D = R[H] === 3
        }
        P.removeChild(R);
        W.cssText = J || "";
        I.cssText = V || ""
    }
    function j(H, G) {
        var J = z(H).y, I = z(G).y;
        return J + H.offsetHeight <= I + G.offsetHeight && J >= I
    }
    function d(H, G) {
        return (z(H).y <= z(G).y)
    }
    function u(H, G) {
        return (z(H).y + H.offsetHeight >= z(G).y + G.offsetHeight)
    }
    j = d = u = null
})();
(function() {
    APE.mixin(APE.dom, {hasToken: f,removeClass: k,addClass: g,hasClass: e,getElementsByClassName: l,findAncestorWithClass: i});
    var h = "className";
    function f(n, m) {
        return b(m, "").test(n)
    }
    function k(n, m) {
        var o = n[h];
        if (!o) {
            return
        }
        if (o === m) {
            n[h] = "";
            return
        }
        n[h] = c(o.replace(b(m, "g"), " "))
    }
    function g(n, m) {
        if (!n[h]) {
            n[h] = m
        }
        if (!b(m).test(n[h])) {
            n[h] += " " + m
        }
    }
    function e(n, m) {
        if (!n[h]) {
            return false
        }
        return f(n[h], m)
    }
    var a = {};
    function b(n, m) {
        var o = n + "$" + m;
        return (a[o] || (a[o] = RegExp("(?:^|\\s)" + n + "(?:$|\\s)", m)))
    }
    function l(n, o, u) {
        if (!u) {
            return []
        }
        o = o || "*";
        if (n.getElementsByClassName && (o === "*")) {
            return n.getElementsByClassName(u)
        }
        var p = b(u, ""), r = n.getElementsByTagName(o), s = r.length, m = 0, q, t = Array(s);
        for (q = 0; q < s; q++) {
            if (p.test(r[q][h])) {
                t[m++] = r[q]
            }
        }
        t.length = m;
        return t
    }
    function i(p, m, n) {
        if (p == null || p === n) {
            return null
        }
        var q = b(m, ""), o;
        for (o = p.parentNode; o != n; ) {
            if (q.test(o[h])) {
                return o
            }
            o = o.parentNode
        }
        return null
    }
    var d = /^\s+|\s+$/g, j = /\s\s+/g;
    function c(m) {
        return m.replace(d, "").replace(j, " ")
    }
})();
(function() {
    var d = document.documentElement, f = "nodeType", e = "tagName", h = "parentNode", b = "compareDocumentPosition", i = /^H/.test(d[e]) ? "toUpperCase" : "toLowerCase", k = /^[A-Z]/;
    APE.mixin(APE.dom, {contains: m(),findAncestorWithAttribute: a,findAncestorWithTagName: n,findNextSiblingElement: c,findPreviousSiblingElement: g,getChildElements: j,isTagName: l});
    function m() {
        if (b in d) {
            return function(p, o) {
                return (p[b](o) & 16) !== 0
            }
        } else {
            if ("contains" in d) {
                return function(p, o) {
                    return p !== o && p.contains(o)
                }
            }
        }
        return function(p, o) {
            if (p === o) {
                return false
            }
            while (p != o && (o = o[h]) !== null) {
            }
            return p === o
        }
    }
    function a(q, t, r) {
        for (var s, p = q[h]; p !== null; ) {
            s = p.attributes;
            if (!s) {
                return null
            }
            var o = s[t];
            if (o && o.specified) {
                if (o.value === r || (r === undefined)) {
                    return p
                }
            }
            p = p[h]
        }
        return null
    }
    function n(q, o) {
        o = o[i]();
        for (var p = q[h]; p !== null; ) {
            if (p[e] === o) {
                return p
            }
            p = p[h]
        }
        return null
    }
    function c(p) {
        for (var o = p.nextSibling; o !== null; o = o.nextSibling) {
            if (o[f] === 1) {
                return o
            }
        }
        return null
    }
    function g(o) {
        for (var p = o.previousSibling; p !== null; p = p.previousSibling) {
            if (p[f] === 1) {
                return p
            }
        }
        return null
    }
    function j(s) {
        var r = 0, q = [], p, o, u = s.children || s.childNodes, t;
        for (p = u.length; r < p; r++) {
            t = u[r];
            if (t[f] !== 1) {
                continue
            }
            q[q.length] = t
        }
        return q
    }
    function l(p, o) {
        return p.tagName == o[i]()
    }
})();
(function() {
    var a = "addEventListener" in this, g = a ? "target" : "srcElement";
    APE.mixin(APE.dom.Event = {}, {eventTarget: g,getTarget: e,addCallback: f,removeCallback: h,preventDefault: c,stopPropagation: b});
    function b(j) {
        j = j || window.event;
        var i;
        if (typeof j.stopPropagation == "function") {
            i = function(k) {
                k.stopPropagation()
            }
        } else {
            if ("cancelBubble" in j) {
                i = function(k) {
                    k = k || window.event;
                    k.cancelBubble = true
                }
            }
        }
        (APE.dom.Event.stopPropagation = i)(j)
    }
    function e(i) {
        return (i || event)[g]
    }
    function d(j, i) {
        return a ? i : function(k) {
            i.call(j, k)
        }
    }
    function f(l, k, i) {
        if (a) {
            l.addEventListener(k, i, false)
        } else {
            var j = d(l, i);
            l.attachEvent("on" + k, j)
        }
        return j || i
    }
    function h(k, j, i) {
        if (a) {
            k.removeEventListener(j, i, false)
        } else {
            k.detachEvent("on" + j, i)
        }
        return i
    }
    function c(i) {
        i = i || event;
        if (typeof i.preventDefault == "function") {
            i.preventDefault()
        } else {
            if ("returnValue" in i) {
                i.returnValue = false
            }
        }
    }
})();
APE.namespace("APE.dom.Event");
(function() {
    var c = APE.dom, a = c.Event;
    a.getCoords = b;
    function b(g) {
        var d;
        if ("pageX" in g) {
            d = function(f) {
                return {x: f.pageX,y: f.pageY}
            }
        } else {
            d = function(h) {
                var f = c.getScrollOffsets();
                h = h || window.event;
                return {x: h.clientX + f.left,y: h.clientY + f.top}
            }
        }
        return (a.getCoords = d)(g)
    }
})();
(function() {
    var p = /^(?:margin|(border)(Width)|padding)$/, d = /^[a-zA-Z]*[bB]orderRadius$/, r = APE.dom;
    APE.mixin(r, {getStyle: e,setOpacity: u,getFilterOpacity: j,multiLengthPropExp: /^(?:margin|(border)(Width)|padding)$/,borderRadiusExp: /^[a-zA-Z]*[bB]orderRadius$/,tryGetShorthandValues: v,getCurrentStyleValueFromAuto: a,getCurrentStyleClipValues: f,convertNonPixelToPixel: b});
    var i = document.defaultView, t = "getComputedStyle", g = r.IS_COMPUTED_STYLE, l = "currentStyle", s = "style";
    i = null;
    function j(w) {
        var x = w.filters;
        if (!x) {
            return ""
        }
        try {
            return x["DXImageTransform.Microsoft.Alpha"].opacity / 100
        } catch (y) {
            try {
                return x("alpha").opacity / 100
            } catch (y) {
                return 1
            }
        }
    }
    function u(z, w) {
        var y = z[s], x;
        if ("opacity" in y) {
            y.opacity = w
        } else {
            if ("filter" in y) {
                x = z[l];
                y.filter = "alpha(opacity=" + (w * 100) + ")";
                if (x && ("hasLayout" in x) && !x.hasLayout) {
                    s.zoom = 1
                }
            }
        }
    }
    function e(x, w) {
        var F = "", C, A, y, z, D, E = x.ownerDocument, B = E.defaultView;
        if (g) {
            C = B[t](x, "");
            if (w == "borderRadius" && !("borderRadius" in C)) {
                w = "MozBorderRadius" in C ? "MozBorderRadius" : "WebkitBorderRadius" in C ? "WebkitBorderRadius" : ""
            }
            if (!(w in C)) {
                return ""
            }
            F = C[w];
            if (F === "") {
                F = (v(C, w)).join(" ")
            }
        } else {
            C = x[l];
            if (w == "opacity" && !("opacity" in x[l])) {
                F = j(x)
            } else {
                if (w == "cssFloat") {
                    w = "styleFloat"
                }
                F = C[w];
                if (w == "clip" && !F && ("clipTop" in C)) {
                    F = f(x, C)
                } else {
                    if (F == "auto") {
                        F = a(x, w)
                    } else {
                        if (!(w in C)) {
                            return ""
                        }
                    }
                }
            }
            A = h.exec(F);
            if (A) {
                y = F.split(" ");
                y[0] = b(x, A);
                for (z = 1, D = y.length; z < D; z++) {
                    A = h.exec(y[z]);
                    y[z] = b(x, A)
                }
                F = y.join(" ")
            }
        }
        return F
    }
    function f(z, y) {
        var w = [], x = 0, A;
        for (; x < 4; x++) {
            A = c[x];
            clipValue = y["clip" + A];
            if (clipValue == "auto") {
                clipValue = (A == "Left" || A == "Top" ? "0px" : A == "Right" ? z.offsetWidth + m : z.offsetHeight + m)
            }
            w.push(clipValue)
        }
        return {top: w[0],right: w[1],bottom: w[2],left: w[3],toString: function() {
                return "rect(" + w.join(" ") + ")"
            }}
    }
    var k = document.documentElement[s], n = "cssFloat" in k ? "cssFloat" : "styleFloat", c = ["Top", "Right", "Bottom", "Left"], o = ["Topright", "Bottomright", "Bottomleft", "Topleft"];
    k = null;
    function a(A, C) {
        var z = A[s], y, x, B = A.ownerDocument;
        if ("pixelWidth" in z && q.test(C)) {
            var w = "pixel" + (C.charAt(0).toUpperCase()) + C.substring(1);
            y = z[w];
            if (y === 0) {
                if (C == "width") {
                    x = parseFloat(e(A, "borderRightWidth")) || 0;
                    paddingWidth = parseFloat(e(A, "paddingLeft")) || 0 + parseFloat(e(A, "paddingRight")) || 0;
                    return A.offsetWidth - A.clientLeft - x - paddingWidth + m
                } else {
                    if (C == "height") {
                        x = parseFloat(e(A, "borderBottomWidth")) || 0;
                        paddingWidth = parseFloat(e(A, "paddingTop")) || 0 + parseFloat(e(A, "paddingBottom")) || 0;
                        return A.offsetHeight - A.clientTop - x + m
                    }
                }
            }
            return z[w] + m
        }
        if (C == "margin" && A[l].position != "absolute" && B.compatMode != "BackCompat") {
            y = parseFloat(e(A.parentNode, "width")) - A.offsetWidth;
            if (y == 0) {
                return "0px"
            }
            y = "0px " + y;
            return y + " " + y
        }
    }
    function v(C, x) {
        var y = p.exec(x), A, G, B, E, F, D = true, w, z = 1;
        if (y && y[0]) {
            w = c;
            A = y[1] || y[0];
            G = y[2] || ""
        } else {
            if (d.test(x)) {
                w = o;
                A = d.exec(x)[0];
                G = ""
            } else {
                return [""]
            }
        }
        B = C[A + w[0] + G];
        F = [B];
        while (z < 4) {
            E = C[A + w[z] + G];
            D = D && E == B;
            B = E;
            F[z++] = E
        }
        if (D) {
            return [B]
        }
        return F
    }
    var h = /(-?\d+|(?:-?\d*\.\d+))(?:em|ex|pt|pc|in|cm|mm\s*)/, q = /width|height|top|left/, m = "px";
    function b(A, B) {
        if (A.runtimeStyle) {
            var C = B[0];
            if (parseFloat(C) == 0) {
                return "0px"
            }
            var z = A[s], y = z.left, x = A.runtimeStyle, w = x.left;
            x.left = A[l].left;
            z.left = (C || 0);
            C = z.pixelLeft + m;
            z.left = y;
            x.left = w;
            return C
        }
    }
})();
(function() {
    var f = document, a = f.body, i, e = "getElementById", h = document[e];
    if (!a) {
        return setTimeout(arguments.callee, 50)
    }
    try {
        i = f.createElement("<A NAME=0>");
        a.insertBefore(i, a.firstChild);
        if (f[e]("0")) {
            a.removeChild(i);
            f[e] = b
        }
    } catch (a) {
    }
    function b(j) {
        var g = Function.prototype.call.call(h, this, j), d, c;
        if (g && g.id == j) {
            return g
        }
        d = this.getElementsByName(j);
        for (c = 0; c < d.length; c++) {
            if (d[c].id === j) {
                return d[c]
            }
        }
        return null
    }
})();
(function() {
    APE.mixin(APE.dom, {selectOptionByValue: a});
    function a(d, e) {
        for (var c = 0, b = d.options.length; c < b; c++) {
            if (d.options[c].value === e) {
                d.selectedIndex = c;
                return
            }
        }
    }
})();
(function() {
    var a = this.document;
    APE.EventPublisher.remove = function(e, d, c, b) {
        return APE.EventPublisher.get(e, d).remove(c, b)
    };
    APE.getElement = function(b) {
        return typeof b === "string" ? a.getElementById(b) : b
    }
})();
Element.addMethods("button", {enable: Field.enable,disable: Field.disable});
Element.addMethods({fade: function(b, a) {
        emile(b, "opacity:0", Object.extend(a || {}, {duration: 500,after: function() {
                b.style.display = "none";
                Element.setOpacity(b, 1)
            }}))
    }});
/*! Fabric.js Copyright 2008-2012, Bitsonnet (Juriy Zaytsev, Maxim Chernyak) */
var fabric = fabric || {version: "0.8"};
if (typeof exports != "undefined") {
    exports.fabric = fabric
}
if (typeof document != "undefined" && typeof window != "undefined") {
    fabric.document = document;
    fabric.window = window
} else {
    fabric.document = require("jsdom").jsdom("<!DOCTYPE html><html><head></head><body></body></html>");
    fabric.window = fabric.document.createWindow()
}
fabric.isTouchSupported = "ontouchstart" in fabric.document.documentElement;
if (!this.JSON) {
    this.JSON = {}
}
(function() {
    function f(n) {
        return n < 10 ? "0" + n : n
    }
    if (typeof Date.prototype.toJSON !== "function") {
        Date.prototype.toJSON = function(key) {
            return isFinite(this.valueOf()) ? this.getUTCFullYear() + "-" + f(this.getUTCMonth() + 1) + "-" + f(this.getUTCDate()) + "T" + f(this.getUTCHours()) + ":" + f(this.getUTCMinutes()) + ":" + f(this.getUTCSeconds()) + "Z" : null
        };
        String.prototype.toJSON = Number.prototype.toJSON = Boolean.prototype.toJSON = function(key) {
            return this.valueOf()
        }
    }
    var cx = /[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g, escapable = /[\\\"\x00-\x1f\x7f-\x9f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g, gap, indent, meta = {"\b": "\\b","\t": "\\t","\n": "\\n","\f": "\\f","\r": "\\r",'"': '\\"',"\\": "\\\\"}, rep;
    function quote(string) {
        escapable.lastIndex = 0;
        return escapable.test(string) ? '"' + string.replace(escapable, function(a) {
            var c = meta[a];
            return typeof c === "string" ? c : "\\u" + ("0000" + a.charCodeAt(0).toString(16)).slice(-4)
        }) + '"' : '"' + string + '"'
    }
    function str(key, holder) {
        var i, k, v, length, mind = gap, partial, value = holder[key];
        if (value && typeof value === "object" && typeof value.toJSON === "function") {
            value = value.toJSON(key)
        }
        if (typeof rep === "function") {
            value = rep.call(holder, key, value)
        }
        switch (typeof value) {
            case "string":
                return quote(value);
            case "number":
                return isFinite(value) ? String(value) : "null";
            case "boolean":
            case "null":
                return String(value);
            case "object":
                if (!value) {
                    return "null"
                }
                gap += indent;
                partial = [];
                if (Object.prototype.toString.apply(value) === "[object Array]") {
                    length = value.length;
                    for (i = 0; i < length; i += 1) {
                        partial[i] = str(i, value) || "null"
                    }
                    v = partial.length === 0 ? "[]" : gap ? "[\n" + gap + partial.join(",\n" + gap) + "\n" + mind + "]" : "[" + partial.join(",") + "]";
                    gap = mind;
                    return v
                }
                if (rep && typeof rep === "object") {
                    length = rep.length;
                    for (i = 0; i < length; i += 1) {
                        k = rep[i];
                        if (typeof k === "string") {
                            v = str(k, value);
                            if (v) {
                                partial.push(quote(k) + (gap ? ": " : ":") + v)
                            }
                        }
                    }
                } else {
                    for (k in value) {
                        if (Object.hasOwnProperty.call(value, k)) {
                            v = str(k, value);
                            if (v) {
                                partial.push(quote(k) + (gap ? ": " : ":") + v)
                            }
                        }
                    }
                }
                v = partial.length === 0 ? "{}" : gap ? "{\n" + gap + partial.join(",\n" + gap) + "\n" + mind + "}" : "{" + partial.join(",") + "}";
                gap = mind;
                return v
        }
    }
    if (typeof JSON.stringify !== "function") {
        JSON.stringify = function(value, replacer, space) {
            var i;
            gap = "";
            indent = "";
            if (typeof space === "number") {
                for (i = 0; i < space; i += 1) {
                    indent += " "
                }
            } else {
                if (typeof space === "string") {
                    indent = space
                }
            }
            rep = replacer;
            if (replacer && typeof replacer !== "function" && (typeof replacer !== "object" || typeof replacer.length !== "number")) {
                throw new Error("JSON.stringify")
            }
            return str("", {"": value})
        }
    }
    if (typeof JSON.parse !== "function") {
        JSON.parse = function(text, reviver) {
            var j;
            function walk(holder, key) {
                var k, v, value = holder[key];
                if (value && typeof value === "object") {
                    for (k in value) {
                        if (Object.hasOwnProperty.call(value, k)) {
                            v = walk(value, k);
                            if (v !== undefined) {
                                value[k] = v
                            } else {
                                delete value[k]
                            }
                        }
                    }
                }
                return reviver.call(holder, key, value)
            }
            text = String(text);
            cx.lastIndex = 0;
            if (cx.test(text)) {
                text = text.replace(cx, function(a) {
                    return "\\u" + ("0000" + a.charCodeAt(0).toString(16)).slice(-4)
                })
            }
            if (/^[\],:{}\s]*$/.test(text.replace(/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g, "@").replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, "]").replace(/(?:^|:|,)(?:\s*\[)+/g, ""))) {
                j = eval("(" + text + ")");
                return typeof reviver === "function" ? walk({"": j}, "") : j
            }
            throw new SyntaxError("JSON.parse")
        }
    }
}());
/*!
 * Copyright (c) 2009 Simo Kinnunen.
 * Licensed under the MIT license.
 */
var Cufon = (function() {
    var k = function() {
        return k.replace.apply(null, arguments)
    };
    var u = k.DOM = {ready: (function() {
            var z = false, B = {loaded: 1,complete: 1};
            var y = [], A = function() {
                if (z) {
                    return
                }
                z = true;
                for (var C; C = y.shift(); C()) {
                }
            };
            if (fabric.document.addEventListener) {
                fabric.document.addEventListener("DOMContentLoaded", A, false);
                fabric.window.addEventListener("pageshow", A, false)
            }
            if (!fabric.window.opera && fabric.document.readyState) {
                (function() {
                    B[fabric.document.readyState] ? A() : setTimeout(arguments.callee, 10)
                })()
            }
            if (fabric.document.readyState && fabric.document.createStyleSheet) {
                (function() {
                    try {
                        fabric.document.body.doScroll("left");
                        A()
                    } catch (C) {
                        setTimeout(arguments.callee, 1)
                    }
                })()
            }
            o(fabric.window, "load", A);
            return function(C) {
                if (!arguments.length) {
                    A()
                } else {
                    z ? C() : y.push(C)
                }
            }
        })()};
    var l = k.CSS = {Size: function(z, y) {
            this.value = parseFloat(z);
            this.unit = String(z).match(/[a-z%]*$/)[0] || "px";
            this.convert = function(A) {
                return A / y * this.value
            };
            this.convertFrom = function(A) {
                return A / this.value * y
            };
            this.toString = function() {
                return this.value + this.unit
            }
        },getStyle: function(y) {
            return new a(y.style)
        },quotedList: i(function(B) {
            var A = [], z = /\s*((["'])([\s\S]*?[^\\])\2|[^,]+)\s*/g, y;
            while (y = z.exec(B)) {
                A.push(y[3] || y[1])
            }
            return A
        }),ready: (function() {
            var A = false;
            var z = [], B = function() {
                A = true;
                for (var D; D = z.shift(); D()) {
                }
            };
            var y = Object.prototype.propertyIsEnumerable ? f("style") : {length: 0};
            var C = f("link");
            u.ready(function() {
                var G = 0, F;
                for (var E = 0, D = C.length; F = C[E], E < D; ++E) {
                    if (!F.disabled && F.rel.toLowerCase() == "stylesheet") {
                        ++G
                    }
                }
                if (fabric.document.styleSheets.length >= y.length + G) {
                    B()
                } else {
                    setTimeout(arguments.callee, 10)
                }
            });
            return function(D) {
                if (A) {
                    D()
                } else {
                    z.push(D)
                }
            }
        })(),supports: function(A, z) {
            var y = fabric.document.createElement("span").style;
            if (y[A] === undefined) {
                return false
            }
            y[A] = z;
            return y[A] === z
        },textAlign: function(B, A, y, z) {
            if (A.get("textAlign") == "right") {
                if (y > 0) {
                    B = " " + B
                }
            } else {
                if (y < z - 1) {
                    B += " "
                }
            }
            return B
        },textDecoration: function(D, C) {
            if (!C) {
                C = this.getStyle(D)
            }
            var z = {underline: null,overline: null,"line-through": null};
            for (var y = D; y.parentNode && y.parentNode.nodeType == 1; ) {
                var B = true;
                for (var A in z) {
                    if (z[A]) {
                        continue
                    }
                    if (C.get("textDecoration").indexOf(A) != -1) {
                        z[A] = C.get("color")
                    }
                    B = false
                }
                if (B) {
                    break
                }
                C = this.getStyle(y = y.parentNode)
            }
            return z
        },textShadow: i(function(C) {
            if (C == "none") {
                return null
            }
            var B = [], D = {}, y, z = 0;
            var A = /(#[a-f0-9]+|[a-z]+\(.*?\)|[a-z]+)|(-?[\d.]+[a-z%]*)|,/ig;
            while (y = A.exec(C)) {
                if (y[0] == ",") {
                    B.push(D);
                    D = {}, z = 0
                } else {
                    if (y[1]) {
                        D.color = y[1]
                    } else {
                        D[["offX", "offY", "blur"][z++]] = y[2]
                    }
                }
            }
            B.push(D);
            return B
        }),color: i(function(z) {
            var y = {};
            y.color = z.replace(/^rgba\((.*?),\s*([\d.]+)\)/, function(B, A, C) {
                y.opacity = parseFloat(C);
                return "rgb(" + A + ")"
            });
            return y
        }),textTransform: function(z, y) {
            return z[{uppercase: "toUpperCase",lowercase: "toLowerCase"}[y.get("textTransform")] || "toString"]()
        }};
    function q(z) {
        var y = this.face = z.face;
        this.glyphs = z.glyphs;
        this.w = z.w;
        this.baseSize = parseInt(y["units-per-em"], 10);
        this.family = y["font-family"].toLowerCase();
        this.weight = y["font-weight"];
        this.style = y["font-style"] || "normal";
        this.viewBox = (function() {
            var B = y.bbox.split(/\s+/);
            var A = {minX: parseInt(B[0], 10),minY: parseInt(B[1], 10),maxX: parseInt(B[2], 10),maxY: parseInt(B[3], 10)};
            A.width = A.maxX - A.minX, A.height = A.maxY - A.minY;
            A.toString = function() {
                return [this.minX, this.minY, this.width, this.height].join(" ")
            };
            return A
        })();
        this.ascent = -parseInt(y.ascent, 10);
        this.descent = -parseInt(y.descent, 10);
        this.height = -this.ascent + this.descent
    }
    function e() {
        var z = {}, y = {oblique: "italic",italic: "oblique"};
        this.add = function(A) {
            (z[A.style] || (z[A.style] = {}))[A.weight] = A
        };
        this.get = function(E, F) {
            var D = z[E] || z[y[E]] || z.normal || z.italic || z.oblique;
            if (!D) {
                return null
            }
            F = {normal: 400,bold: 700}[F] || parseInt(F, 10);
            if (D[F]) {
                return D[F]
            }
            var B = {1: 1,99: 0}[F % 100], H = [], C, A;
            if (B === undefined) {
                B = F > 400
            }
            if (F == 500) {
                F = 400
            }
            for (var G in D) {
                G = parseInt(G, 10);
                if (!C || G < C) {
                    C = G
                }
                if (!A || G > A) {
                    A = G
                }
                H.push(G)
            }
            if (F < C) {
                F = C
            }
            if (F > A) {
                F = A
            }
            H.sort(function(J, I) {
                return (B ? (J > F && I > F) ? J < I : J > I : (J < F && I < F) ? J > I : J < I) ? -1 : 1
            });
            return D[H[0]]
        }
    }
    function p() {
        function A(C, D) {
            if (C.contains) {
                return C.contains(D)
            }
            return C.compareDocumentPosition(D) & 16
        }
        function y(D) {
            var C = D.relatedTarget;
            if (!C || A(this, C)) {
                return
            }
            z(this)
        }
        function B(C) {
            z(this)
        }
        function z(C) {
            setTimeout(function() {
                k.replace(C, d.get(C).options, true)
            }, 10)
        }
        this.attach = function(C) {
            if (C.onmouseenter === undefined) {
                o(C, "mouseover", y);
                o(C, "mouseout", y)
            } else {
                o(C, "mouseenter", B);
                o(C, "mouseleave", B)
            }
        }
    }
    function x() {
        var A = {}, y = 0;
        function z(B) {
            return B.cufid || (B.cufid = ++y)
        }
        this.get = function(B) {
            var C = z(B);
            return A[C] || (A[C] = {})
        }
    }
    function a(y) {
        var A = {}, z = {};
        this.get = function(B) {
            return A[B] != undefined ? A[B] : y[B]
        };
        this.getSize = function(C, B) {
            return z[C] || (z[C] = new l.Size(this.get(C), B))
        };
        this.extend = function(B) {
            for (var C in B) {
                A[C] = B[C]
            }
            return this
        }
    }
    function o(z, y, A) {
        if (z.addEventListener) {
            z.addEventListener(y, A, false)
        } else {
            if (z.attachEvent) {
                z.attachEvent("on" + y, function() {
                    return A.call(z, fabric.window.event)
                })
            }
        }
    }
    function r(z, y) {
        var A = d.get(z);
        if (A.options) {
            return z
        }
        if (y.hover && y.hoverables[z.nodeName.toLowerCase()]) {
            b.attach(z)
        }
        A.options = y;
        return z
    }
    function i(y) {
        var z = {};
        return function(A) {
            if (!z.hasOwnProperty(A)) {
                z[A] = y.apply(null, arguments)
            }
            return z[A]
        }
    }
    function c(D, C) {
        if (!C) {
            C = l.getStyle(D)
        }
        var z = l.quotedList(C.get("fontFamily").toLowerCase()), B;
        for (var A = 0, y = z.length; A < y; ++A) {
            B = z[A];
            if (h[B]) {
                return h[B].get(C.get("fontStyle"), C.get("fontWeight"))
            }
        }
        return null
    }
    function f(y) {
        return fabric.document.getElementsByTagName(y)
    }
    function g() {
        var y = {}, B;
        for (var A = 0, z = arguments.length; A < z; ++A) {
            for (B in arguments[A]) {
                y[B] = arguments[A][B]
            }
        }
        return y
    }
    function m(B, J, z, K, C, A) {
        var I = K.separate;
        if (I == "none") {
            return w[K.engine].apply(null, arguments)
        }
        var H = fabric.document.createDocumentFragment(), E;
        var F = J.split(n[I]), y = (I == "words");
        if (y && s) {
            if (/^\s/.test(J)) {
                F.unshift("")
            }
            if (/\s$/.test(J)) {
                F.push("")
            }
        }
        for (var G = 0, D = F.length; G < D; ++G) {
            E = w[K.engine](B, y ? l.textAlign(F[G], z, G, D) : F[G], z, K, C, A, G < D - 1);
            if (E) {
                H.appendChild(E)
            }
        }
        return H
    }
    function j(z, G) {
        var A, y, D, F;
        for (var B = r(z, G).firstChild; B; B = D) {
            D = B.nextSibling;
            F = false;
            if (B.nodeType == 1) {
                if (!B.firstChild) {
                    continue
                }
                if (!/cufon/.test(B.className)) {
                    arguments.callee(B, G);
                    continue
                } else {
                    F = true
                }
            }
            if (!y) {
                y = l.getStyle(z).extend(G)
            }
            if (!A) {
                A = c(z, y)
            }
            if (!A) {
                continue
            }
            if (F) {
                w[G.engine](A, null, y, G, B, z);
                continue
            }
            var E = B.data;
            if (typeof G_vmlCanvasManager != "undefined") {
                E = E.replace(/\r/g, "\n")
            }
            if (E === "") {
                continue
            }
            var C = m(A, E, y, G, B, z);
            if (C) {
                B.parentNode.replaceChild(C, B)
            } else {
                B.parentNode.removeChild(B)
            }
        }
    }
    var s = " ".split(/\s+/).length == 0;
    var d = new x();
    var b = new p();
    var v = [];
    var w = {}, h = {}, t = {engine: null,hover: false,hoverables: {a: true},printable: true,selector: (fabric.window.Sizzle || (fabric.window.jQuery && function(y) {
            return jQuery(y)
        }) || (fabric.window.dojo && dojo.query) || (fabric.window.$$ && function(y) {
            return $$(y)
        }) || (fabric.window.$ && function(y) {
            return $(y)
        }) || (fabric.document.querySelectorAll && function(y) {
            return fabric.document.querySelectorAll(y)
        }) || f),separate: "words",textShadow: "none"};
    var n = {words: /\s+/,characters: ""};
    k.now = function() {
        u.ready();
        return k
    };
    k.refresh = function() {
        var A = v.splice(0, v.length);
        for (var z = 0, y = A.length; z < y; ++z) {
            k.replace.apply(null, A[z])
        }
        return k
    };
    k.registerEngine = function(z, y) {
        if (!y) {
            return k
        }
        w[z] = y;
        return k.set("engine", z)
    };
    k.registerFont = function(A) {
        var y = new q(A), z = y.family;
        if (!h[z]) {
            h[z] = new e()
        }
        h[z].add(y);
        return k.set("fontFamily", '"' + z + '"')
    };
    k.replace = function(A, z, y) {
        z = g(t, z);
        if (!z.engine) {
            return k
        }
        if (typeof z.textShadow == "string" && z.textShadow) {
            z.textShadow = l.textShadow(z.textShadow)
        }
        if (!y) {
            v.push(arguments)
        }
        if (A.nodeType || typeof A == "string") {
            A = [A]
        }
        l.ready(function() {
            for (var C = 0, B = A.length; C < B; ++C) {
                var D = A[C];
                if (typeof D == "string") {
                    k.replace(z.selector(D), z, true)
                } else {
                    j(D, z)
                }
            }
        });
        return k
    };
    k.replaceElement = function(z, y) {
        y = g(t, y);
        if (typeof y.textShadow == "string" && y.textShadow) {
            y.textShadow = l.textShadow(y.textShadow)
        }
        return j(z, y)
    };
    k.engines = w;
    k.fonts = h;
    k.getOptions = function() {
        return g(t)
    };
    k.set = function(y, z) {
        t[y] = z;
        return k
    };
    return k
})();
Cufon.registerEngine("canvas", (function() {
    var a = Cufon.CSS.supports("display", "inline-block");
    var g = !a && (fabric.document.compatMode == "BackCompat" || /frameset|transitional/i.test(fabric.document.doctype.publicId));
    var h = fabric.document.createElement("style");
    h.type = "text/css";
    var f = fabric.document.createTextNode(".cufon-canvas{text-indent:0}@media screen,projection{.cufon-canvas{display:inline;display:inline-block;position:relative;vertical-align:middle" + (g ? "" : ";font-size:1px;line-height:1px") + "}.cufon-canvas .cufon-alt{display:-moz-inline-box;display:inline-block;width:0;height:0;overflow:hidden}" + (a ? ".cufon-canvas canvas{position:relative}" : ".cufon-canvas canvas{position:absolute}") + "}@media print{.cufon-canvas{padding:0 !important}.cufon-canvas canvas{display:none}.cufon-canvas .cufon-alt{display:inline}}");
    try {
        h.appendChild(f)
    } catch (d) {
        h.setAttribute("type", "text/css");
        h.styleSheet.cssText = f.data
    }
    fabric.document.getElementsByTagName("head")[0].appendChild(h);
    function c(q, j) {
        var o = 0, n = 0;
        var e = [], p = /([mrvxe])([^a-z]*)/g, l;
        generate: for (var k = 0; l = p.exec(q); ++k) {
            var m = l[2].split(",");
            switch (l[1]) {
                case "v":
                    e[k] = {m: "bezierCurveTo",a: [o + ~~m[0], n + ~~m[1], o + ~~m[2], n + ~~m[3], o += ~~m[4], n += ~~m[5]]};
                    break;
                case "r":
                    e[k] = {m: "lineTo",a: [o += ~~m[0], n += ~~m[1]]};
                    break;
                case "m":
                    e[k] = {m: "moveTo",a: [o = ~~m[0], n = ~~m[1]]};
                    break;
                case "x":
                    e[k] = {m: "closePath",a: []};
                    break;
                case "e":
                    break generate
            }
            j[e[k].m].apply(j, e[k].a)
        }
        return e
    }
    function b(n, m) {
        for (var k = 0, j = n.length; k < j; ++k) {
            var e = n[k];
            m[e.m].apply(m, e.a)
        }
    }
    return function(ab, E, W, A, J, ac) {
        var n = (E === null);
        var H = ab.viewBox;
        var o = W.getSize("fontSize", ab.baseSize);
        var U = W.get("letterSpacing");
        U = (U == "normal") ? 0 : o.convertFrom(parseInt(U, 10));
        var I = 0, V = 0, T = 0, C = 0;
        var G = A.textShadow, R = [];
        Cufon.textOptions.shadowOffsets = [];
        Cufon.textOptions.shadows = null;
        if (G) {
            Cufon.textOptions.shadows = G;
            for (var aa = 0, X = G.length; aa < X; ++aa) {
                var N = G[aa];
                var Q = o.convertFrom(parseFloat(N.offX));
                var P = o.convertFrom(parseFloat(N.offY));
                R[aa] = [Q, P];
                if (P < I) {
                    I = P
                }
                if (Q > V) {
                    V = Q
                }
                if (P > T) {
                    T = P
                }
                if (Q < C) {
                    C = Q
                }
            }
        }
        var ag = Cufon.CSS.textTransform(n ? J.alt : E, W).split("");
        var e = 0, D = null;
        var z = 0, M = 1, L = [];
        for (var aa = 0, X = ag.length; aa < X; ++aa) {
            if (ag[aa] === "\n") {
                M++;
                if (e > z) {
                    z = e
                }
                L.push(e);
                e = 0;
                continue
            }
            var B = ab.glyphs[ag[aa]] || ab.missingGlyph;
            if (!B) {
                continue
            }
            e += D = Number(B.w || ab.w) + U
        }
        L.push(e);
        e = Math.max(z, e);
        var m = [];
        for (var aa = L.length; aa--; ) {
            m[aa] = e - L[aa]
        }
        if (D === null) {
            return null
        }
        V += (H.width - D);
        C += H.minX;
        var v, p;
        if (n) {
            v = J;
            p = J.firstChild
        } else {
            v = fabric.document.createElement("span");
            v.className = "cufon cufon-canvas";
            v.alt = E;
            p = fabric.document.createElement("canvas");
            v.appendChild(p);
            if (A.printable) {
                var Y = fabric.document.createElement("span");
                Y.className = "cufon-alt";
                Y.appendChild(fabric.document.createTextNode(E));
                v.appendChild(Y)
            }
        }
        var ah = v.style;
        var O = p.style || {};
        var k = o.convert(H.height - I + T);
        var af = Math.ceil(k);
        var S = af / k;
        p.width = Math.ceil(o.convert(e + V - C) * S);
        p.height = af;
        I += H.minY;
        O.top = Math.round(o.convert(I - ab.ascent)) + "px";
        O.left = Math.round(o.convert(C)) + "px";
        var j = Math.ceil(o.convert(e * S));
        var t = j + "px";
        var s = o.convert(ab.height);
        var F = (A.lineHeight - 1) * o.convert(-ab.ascent / 5) * (M - 1);
        Cufon.textOptions.width = j;
        Cufon.textOptions.height = (s * M) + F;
        Cufon.textOptions.lines = M;
        Cufon.textOptions.totalLineHeight = F;
        if (a) {
            ah.width = t;
            ah.height = s + "px"
        } else {
            ah.paddingLeft = t;
            ah.paddingBottom = (s - 1) + "px"
        }
        var ad = Cufon.textOptions.context || p.getContext("2d"), K = af / H.height;
        Cufon.textOptions.fontAscent = ab.ascent * K;
        Cufon.textOptions.boundaries = null;
        for (var w = Cufon.textOptions.shadowOffsets, aa = R.length; aa--; ) {
            w[aa] = [R[aa][0] * K, R[aa][1] * K]
        }
        ad.save();
        ad.scale(K, K);
        ad.translate(-C - ((1 / K * p.width) / 2) + (Cufon.fonts[ab.family].offsetLeft || 0), -I - (Cufon.textOptions.height / K) / 2);
        ad.lineWidth = ab.face["underline-thickness"];
        ad.save();
        function q(l, i) {
            ad.strokeStyle = i;
            ad.beginPath();
            ad.moveTo(0, l);
            ad.lineTo(e, l);
            ad.stroke()
        }
        var r = Cufon.getTextDecoration(A), u = A.fontStyle === "italic";
        function ae() {
            ad.save();
            ad.fillStyle = A.backgroundColor;
            var aj = 0, an = 0, y = [{left: 0}];
            if (A.textAlign === "right") {
                ad.translate(m[an], 0);
                y[0].left = m[an] * K
            } else {
                if (A.textAlign === "center") {
                    ad.translate(m[an] / 2, 0);
                    y[0].left = m[an] / 2 * K
                }
            }
            for (var al = 0, ak = ag.length; al < ak; ++al) {
                if (ag[al] === "\n") {
                    an++;
                    var am = -ab.ascent - ((ab.ascent / 5) * A.lineHeight);
                    var x = y[y.length - 1];
                    var ai = {left: 0};
                    x.width = aj * K;
                    x.height = (-ab.ascent + ab.descent) * K;
                    if (A.textAlign === "right") {
                        ad.translate(-e, am);
                        ad.translate(m[an], 0);
                        ai.left = m[an] * K
                    } else {
                        if (A.textAlign === "center") {
                            ad.translate(-aj - (m[an - 1] / 2), am);
                            ad.translate(m[an] / 2, 0);
                            ai.left = m[an] / 2 * K
                        } else {
                            ad.translate(-aj, am)
                        }
                    }
                    y.push(ai);
                    aj = 0;
                    continue
                }
                var ap = ab.glyphs[ag[al]] || ab.missingGlyph;
                if (!ap) {
                    continue
                }
                var ao = Number(ap.w || ab.w) + U;
                if (A.backgroundColor) {
                    ad.save();
                    ad.translate(0, ab.ascent);
                    ad.fillRect(0, 0, ao + 10, -ab.ascent + ab.descent);
                    ad.restore()
                }
                ad.translate(ao, 0);
                aj += ao;
                if (al == ak - 1) {
                    y[y.length - 1].width = aj * K;
                    y[y.length - 1].height = (-ab.ascent + ab.descent) * K
                }
            }
            ad.restore();
            Cufon.textOptions.boundaries = y
        }
        function Z(ai) {
            ad.fillStyle = ai || Cufon.textOptions.color || W.get("color");
            var am = 0, an = 0;
            if (A.textAlign === "right") {
                ad.translate(m[an], 0)
            } else {
                if (A.textAlign === "center") {
                    ad.translate(m[an] / 2, 0)
                }
            }
            for (var ak = 0, y = ag.length; ak < y; ++ak) {
                if (ag[ak] === "\n") {
                    an++;
                    var aj = -ab.ascent - ((ab.ascent / 5) * A.lineHeight);
                    if (A.textAlign === "right") {
                        ad.translate(-e, aj);
                        ad.translate(m[an], 0)
                    } else {
                        if (A.textAlign === "center") {
                            ad.translate(-am - (m[an - 1] / 2), aj);
                            ad.translate(m[an] / 2, 0)
                        } else {
                            ad.translate(-am, aj)
                        }
                    }
                    am = 0;
                    continue
                }
                var al = ab.glyphs[ag[ak]] || ab.missingGlyph;
                if (!al) {
                    continue
                }
                var x = Number(al.w || ab.w) + U;
                if (r) {
                    ad.save();
                    ad.strokeStyle = ad.fillStyle;
                    ad.lineWidth += ad.lineWidth;
                    ad.beginPath();
                    if (r.underline) {
                        ad.moveTo(0, -ab.face["underline-position"] + 0.5);
                        ad.lineTo(x, -ab.face["underline-position"] + 0.5)
                    }
                    if (r.overline) {
                        ad.moveTo(0, ab.ascent + 0.5);
                        ad.lineTo(x, ab.ascent + 0.5)
                    }
                    if (r["line-through"]) {
                        ad.moveTo(0, -ab.descent + 0.5);
                        ad.lineTo(x, -ab.descent + 0.5)
                    }
                    ad.stroke();
                    ad.restore()
                }
                if (u) {
                    ad.save();
                    ad.transform(1, 0, -0.25, 1, 0, 0)
                }
                ad.beginPath();
                if (al.d) {
                    if (al.code) {
                        b(al.code, ad)
                    } else {
                        al.code = c("m" + al.d, ad)
                    }
                }
                ad.fill();
                if (A.strokeStyle) {
                    ad.closePath();
                    ad.save();
                    ad.lineWidth = A.strokeWidth;
                    ad.strokeStyle = A.strokeStyle;
                    ad.stroke();
                    ad.restore()
                }
                if (u) {
                    ad.restore()
                }
                ad.translate(x, 0);
                am += x
            }
        }
        if (G) {
            for (var aa = 0, X = G.length; aa < X; ++aa) {
                var N = G[aa];
                ad.save();
                ad.translate.apply(ad, R[aa]);
                Z(N.color);
                ad.restore()
            }
        }
        ad.save();
        ae();
        Z();
        ad.restore();
        ad.restore();
        ad.restore();
        return v
    }
})());
Cufon.registerEngine("vml", (function() {
    if (!fabric.document.namespaces) {
        return
    }
    var d = fabric.document.createElement("canvas");
    if (d && d.getContext && d.getContext.apply) {
        return
    }
    if (fabric.document.namespaces.cvml == null) {
        fabric.document.namespaces.add("cvml", "urn:schemas-microsoft-com:vml")
    }
    var b = fabric.document.createElement("cvml:shape");
    b.style.behavior = "url(#default#VML)";
    if (!b.coordsize) {
        return
    }
    b = null;
    fabric.document.write('<style type="text/css">.cufon-vml-canvas{text-indent:0}@media screen{cvml\\:shape,cvml\\:shadow{behavior:url(#default#VML);display:block;antialias:true;position:absolute}.cufon-vml-canvas{position:absolute;text-align:left}.cufon-vml{display:inline-block;position:relative;vertical-align:middle}.cufon-vml .cufon-alt{position:absolute;left:-10000in;font-size:1px}a .cufon-vml{cursor:pointer}}@media print{.cufon-vml *{display:none}.cufon-vml .cufon-alt{display:inline}}</style>');
    function c(e, f) {
        return a(e, /(?:em|ex|%)$/i.test(f) ? "1em" : f)
    }
    function a(h, i) {
        if (/px$/i.test(i)) {
            return parseFloat(i)
        }
        var g = h.style.left, f = h.runtimeStyle.left;
        h.runtimeStyle.left = h.currentStyle.left;
        h.style.left = i;
        var e = h.style.pixelLeft;
        h.style.left = g;
        h.runtimeStyle.left = f;
        return e
    }
    return function(T, z, O, w, D, U, M) {
        var h = (z === null);
        if (h) {
            z = D.alt
        }
        var B = T.viewBox;
        var j = O.computedFontSize || (O.computedFontSize = new Cufon.CSS.Size(c(U, O.get("fontSize")) + "px", T.baseSize));
        var L = O.computedLSpacing;
        if (L == undefined) {
            L = O.get("letterSpacing");
            O.computedLSpacing = L = (L == "normal") ? 0 : ~~j.convertFrom(a(U, L))
        }
        var t, m;
        if (h) {
            t = D;
            m = D.firstChild
        } else {
            t = fabric.document.createElement("span");
            t.className = "cufon cufon-vml";
            t.alt = z;
            m = fabric.document.createElement("span");
            m.className = "cufon-vml-canvas";
            t.appendChild(m);
            if (w.printable) {
                var R = fabric.document.createElement("span");
                R.className = "cufon-alt";
                R.appendChild(fabric.document.createTextNode(z));
                t.appendChild(R)
            }
            if (!M) {
                t.appendChild(fabric.document.createElement("cvml:shape"))
            }
        }
        var Z = t.style;
        var G = m.style;
        var f = j.convert(B.height), W = Math.ceil(f);
        var K = W / f;
        var J = B.minX, I = B.minY;
        G.height = W;
        G.top = Math.round(j.convert(I - T.ascent));
        G.left = Math.round(j.convert(J));
        Z.height = j.convert(T.height) + "px";
        var p = Cufon.getTextDecoration(w);
        var y = O.get("color");
        var X = Cufon.CSS.textTransform(z, O).split("");
        var e = 0, H = 0, q = null;
        var x, r, A = w.textShadow;
        for (var S = 0, Q = 0, P = X.length; S < P; ++S) {
            x = T.glyphs[X[S]] || T.missingGlyph;
            if (x) {
                e += q = ~~(x.w || T.w) + L
            }
        }
        if (q === null) {
            return null
        }
        var s = -J + e + (B.width - q);
        var Y = j.convert(s * K), N = Math.round(Y);
        var F = s + "," + B.height, g;
        var C = "r" + F + "nsnf";
        for (S = 0; S < P; ++S) {
            x = T.glyphs[X[S]] || T.missingGlyph;
            if (!x) {
                continue
            }
            if (h) {
                r = m.childNodes[Q];
                if (r.firstChild) {
                    r.removeChild(r.firstChild)
                }
            } else {
                r = fabric.document.createElement("cvml:shape");
                m.appendChild(r)
            }
            r.stroked = "f";
            r.coordsize = F;
            r.coordorigin = g = (J - H) + "," + I;
            r.path = (x.d ? "m" + x.d + "xe" : "") + "m" + g + C;
            r.fillcolor = y;
            var V = r.style;
            V.width = N;
            V.height = W;
            if (A) {
                var o = A[0], n = A[1];
                var v = Cufon.CSS.color(o.color), u;
                var E = fabric.document.createElement("cvml:shadow");
                E.on = "t";
                E.color = v.color;
                E.offset = o.offX + "," + o.offY;
                if (n) {
                    u = Cufon.CSS.color(n.color);
                    E.type = "double";
                    E.color2 = u.color;
                    E.offset2 = n.offX + "," + n.offY
                }
                E.opacity = v.opacity || (u && u.opacity) || 1;
                r.appendChild(E)
            }
            H += ~~(x.w || T.w) + L;
            ++Q
        }
        Z.width = Math.max(Math.ceil(j.convert(e * K)), 0);
        return t
    }
})());
Cufon.getTextDecoration = function(a) {
    return {underline: a.textDecoration === "underline",overline: a.textDecoration === "overline","line-through": a.textDecoration === "line-through"}
};
if (typeof exports != "undefined") {
    exports.Cufon = Cufon
}
fabric.log = function() {
};
fabric.warn = function() {
};
if (typeof console !== "undefined") {
    if (typeof console.log !== "undefined" && console.log.apply) {
        fabric.log = function() {
            return console.log.apply(console, arguments)
        }
    }
    if (typeof console.warn !== "undefined" && console.warn.apply) {
        fabric.warn = function() {
            return console.warn.apply(console, arguments)
        }
    }
}
fabric.Observable = {observe: function(a, b) {
        if (!this.__eventListeners) {
            this.__eventListeners = {}
        }
        if (arguments.length === 1) {
            for (var c in a) {
                this.observe(c, a[c])
            }
        } else {
            if (!this.__eventListeners[a]) {
                this.__eventListeners[a] = []
            }
            this.__eventListeners[a].push(b)
        }
    },stopObserving: function(a, b) {
        if (!this.__eventListeners) {
            this.__eventListeners = {}
        }
        if (this.__eventListeners[a]) {
            fabric.util.removeFromArray(this.__eventListeners[a], b)
        }
    },fire: function(d, c) {
        if (!this.__eventListeners) {
            this.__eventListeners = {}
        }
        var b = this.__eventListeners[d];
        if (!b) {
            return
        }
        for (var e = 0, a = b.length; e < a; e++) {
            b[e]({memo: c})
        }
    }};
(function() {
    fabric.util = {};
    function g(l, k) {
        var j = l.indexOf(k);
        if (j !== -1) {
            l.splice(j, 1)
        }
        return l
    }
    function e(k, j) {
        return Math.floor(Math.random() * (j - k + 1)) + k
    }
    var f = Math.PI / 180;
    function b(j) {
        return j * f
    }
    function d(k, j) {
        return parseFloat(Number(k).toFixed(j))
    }
    function i() {
        return false
    }
    function c(u) {
        u || (u = {});
        var k = +new Date(), m = u.duration || 500, t = k + m, l, s, p = u.onChange || function() {
        }, n = u.abort || function() {
            return false
        }, q = u.easing || function(v) {
            return (-Math.cos(v * Math.PI) / 2) + 0.5
        }, j = "startValue" in u ? u.startValue : 0, r = "endValue" in u ? u.endValue : 100;
        u.onStart && u.onStart();
        (function o() {
            l = +new Date();
            s = l > t ? 1 : (l - k) / m;
            p(j + (r - j) * q(s));
            if (l > t || n()) {
                u.onComplete && u.onComplete();
                return
            }
            h(o)
        })()
    }
    var h = (function() {
        return fabric.window.requestAnimationFrame || fabric.window.webkitRequestAnimationFrame || fabric.window.mozRequestAnimationFrame || fabric.window.oRequestAnimationFrame || fabric.window.msRequestAnimationFrame || function(k, j) {
            fabric.window.setTimeout(k, 1000 / 60)
        }
    })();
    function a(k, m, l) {
        if (k) {
            var j = new Image();
            j.onload = function() {
                m && m.call(l, j);
                j = j.onload = null
            };
            j.src = k
        } else {
            m && m.call(l, k)
        }
    }
    fabric.util.removeFromArray = g;
    fabric.util.degreesToRadians = b;
    fabric.util.toFixed = d;
    fabric.util.getRandomInt = e;
    fabric.util.falseFunction = i;
    fabric.util.animate = c;
    fabric.util.requestAnimFrame = h;
    fabric.util.loadImage = a
})();
(function() {
    var d = Array.prototype.slice;
    if (!Array.prototype.indexOf) {
        Array.prototype.indexOf = function(g) {
            if (this === void 0 || this === null) {
                throw new TypeError()
            }
            var h = Object(this), e = h.length >>> 0;
            if (e === 0) {
                return -1
            }
            var i = 0;
            if (arguments.length > 0) {
                i = Number(arguments[1]);
                if (i !== i) {
                    i = 0
                } else {
                    if (i !== 0 && i !== (1 / 0) && i !== -(1 / 0)) {
                        i = (i > 0 || -1) * Math.floor(Math.abs(i))
                    }
                }
            }
            if (i >= e) {
                return -1
            }
            var f = i >= 0 ? i : Math.max(e - Math.abs(i), 0);
            for (; f < e; f++) {
                if (f in h && h[f] === g) {
                    return f
                }
            }
            return -1
        }
    }
    if (!Array.prototype.forEach) {
        Array.prototype.forEach = function(h, g) {
            for (var f = 0, e = this.length >>> 0; f < e; f++) {
                if (f in this) {
                    h.call(g, this[f], f, this)
                }
            }
        }
    }
    if (!Array.prototype.map) {
        Array.prototype.map = function(j, h) {
            var f = [];
            for (var g = 0, e = this.length >>> 0; g < e; g++) {
                if (g in this) {
                    f[g] = j.call(h, this[g], g, this)
                }
            }
            return f
        }
    }
    if (!Array.prototype.every) {
        Array.prototype.every = function(h, g) {
            for (var f = 0, e = this.length >>> 0; f < e; f++) {
                if (f in this && !h.call(g, this[f], f, this)) {
                    return false
                }
            }
            return true
        }
    }
    if (!Array.prototype.some) {
        Array.prototype.some = function(h, g) {
            for (var f = 0, e = this.length >>> 0; f < e; f++) {
                if (f in this && h.call(g, this[f], f, this)) {
                    return true
                }
            }
            return false
        }
    }
    if (!Array.prototype.filter) {
        Array.prototype.filter = function(j, h) {
            var f = [], k;
            for (var g = 0, e = this.length >>> 0; g < e; g++) {
                if (g in this) {
                    k = this[g];
                    if (j.call(h, k, g, this)) {
                        f.push(k)
                    }
                }
            }
            return f
        }
    }
    if (!Array.prototype.reduce) {
        Array.prototype.reduce = function(g) {
            var e = this.length >>> 0, f = 0, h;
            if (arguments.length > 1) {
                h = arguments[1]
            } else {
                do {
                    if (f in this) {
                        h = this[f++];
                        break
                    }
                    if (++f >= e) {
                        throw new TypeError()
                    }
                } while (true)
            }
            for (; f < e; f++) {
                if (f in this) {
                    h = g.call(null, h, this[f], f, this)
                }
            }
            return h
        }
    }
    function b(k, j) {
        var g = d.call(arguments, 2), f = [];
        for (var h = 0, e = k.length; h < e; h++) {
            f[h] = g.length ? k[h][j].apply(k[h], g) : k[h][j].call(k[h])
        }
        return f
    }
    function a(h, g) {
        if (!h || h.length === 0) {
            return undefined
        }
        var f = h.length - 1, e = g ? h[f][g] : h[f];
        if (g) {
            while (f--) {
                if (h[f][g] >= e) {
                    e = h[f][g]
                }
            }
        } else {
            while (f--) {
                if (h[f] >= e) {
                    e = h[f]
                }
            }
        }
        return e
    }
    function c(h, g) {
        if (!h || h.length === 0) {
            return undefined
        }
        var f = h.length - 1, e = g ? h[f][g] : h[f];
        if (g) {
            while (f--) {
                if (h[f][g] < e) {
                    e = h[f][g]
                }
            }
        } else {
            while (f--) {
                if (h[f] < e) {
                    e = h[f]
                }
            }
        }
        return e
    }
    fabric.util.array = {invoke: b,min: c,max: a}
})();
(function() {
    function b(c, e) {
        for (var d in e) {
            c[d] = e[d]
        }
        return c
    }
    function a(c) {
        return b({}, c)
    }
    fabric.util.object = {extend: b,clone: a}
})();
(function() {
    if (!String.prototype.trim) {
        String.prototype.trim = function() {
            return this.replace(/^[\s\xA0]+/, "").replace(/[\s\xA0]+$/, "")
        }
    }
    function b(d) {
        return d.replace(/-+(.)?/g, function(e, f) {
            return f ? f.toUpperCase() : ""
        })
    }
    function c(d) {
        return d.charAt(0).toUpperCase() + d.slice(1).toLowerCase()
    }
    function a(d) {
        return d.replace("&", "&amp;").replace('"', "&quot;").replace("'", "&apos;").replace("<", "&lt;").replace(">", "&gt;")
    }
    fabric.util.string = {camelize: b,capitalize: c,escapeXml: a}
}());
(function() {
    var c = Array.prototype.slice, a = Function.prototype.apply, b = function() {
    };
    if (!Function.prototype.bind) {
        Function.prototype.bind = function(d) {
            var g = this, e = c.call(arguments, 1), f;
            if (e.length) {
                f = function() {
                    return a.call(g, this instanceof b ? this : d, e.concat(c.call(arguments)))
                }
            } else {
                f = function() {
                    return a.call(g, this instanceof b ? this : d, arguments)
                }
            }
            b.prototype = this.prototype;
            f.prototype = new b;
            return f
        }
    }
})();
(function() {
    var f = Array.prototype.slice, e = function() {
    };
    var c = (function() {
        for (var g in {toString: 1}) {
            if (g === "toString") {
                return false
            }
        }
        return true
    })();
    var b;
    if (c) {
        b = function(g, i) {
            if (i.toString !== Object.prototype.toString) {
                g.prototype.toString = i.toString
            }
            if (i.valueOf !== Object.prototype.valueOf) {
                g.prototype.valueOf = i.valueOf
            }
            for (var h in i) {
                g.prototype[h] = i[h]
            }
        }
    } else {
        b = function(g, i) {
            for (var h in i) {
                g.prototype[h] = i[h]
            }
        }
    }
    function a() {
    }
    function d() {
        var k = null, j = f.call(arguments, 0);
        if (typeof j[0] === "function") {
            k = j.shift()
        }
        function g() {
            this.initialize.apply(this, arguments)
        }
        g.superclass = k;
        g.subclasses = [];
        if (k) {
            a.prototype = k.prototype;
            g.prototype = new a;
            k.subclasses.push(g)
        }
        for (var h = 0, l = j.length; h < l; h++) {
            b(g, j[h])
        }
        if (!g.prototype.initialize) {
            g.prototype.initialize = e
        }
        g.prototype.constructor = g;
        return g
    }
    fabric.util.createClass = d
})();
(function(d) {
    function h(s) {
        var w = Array.prototype.slice.call(arguments, 1), v, u, r = w.length;
        for (u = 0; u < r; u++) {
            v = typeof s[w[u]];
            if (!(/^(?:function|object|unknown)$/).test(v)) {
                return false
            }
        }
        return true
    }
    var e = (function() {
        if (typeof fabric.document.documentElement.uniqueID !== "undefined") {
            return function(s) {
                return s.uniqueID
            }
        }
        var r = 0;
        return function(s) {
            return s.__uniqueID || (s.__uniqueID = "uniqueID__" + r++)
        }
    })();
    var f, c;
    (function() {
        var r = {};
        f = function(s) {
            return r[s]
        };
        c = function(t, s) {
            r[t] = s
        }
    })();
    function i(r, s) {
        return {handler: s,wrappedHandler: l(r, s)}
    }
    function l(r, s) {
        return function(t) {
            s.call(f(r), t || fabric.window.event)
        }
    }
    function j(s, r) {
        return function(w) {
            if (g[s] && g[s][r]) {
                var u = g[s][r];
                for (var v = 0, t = u.length; v < t; v++) {
                    u[v].call(this, w || fabric.window.event)
                }
            }
        }
    }
    var o = (h(fabric.document.documentElement, "addEventListener", "removeEventListener") && h(fabric.window, "addEventListener", "removeEventListener")), m = (h(fabric.document.documentElement, "attachEvent", "detachEvent") && h(fabric.window, "attachEvent", "detachEvent")), n = {}, g = {}, b, k;
    if (o) {
        b = function(s, r, t) {
            s.addEventListener(r, t, false)
        };
        k = function(s, r, t) {
            s.removeEventListener(r, t, false)
        }
    } else {
        if (m) {
            b = function(t, r, u) {
                var s = e(t);
                c(s, t);
                if (!n[s]) {
                    n[s] = {}
                }
                if (!n[s][r]) {
                    n[s][r] = []
                }
                var v = i(s, u);
                n[s][r].push(v);
                t.attachEvent("on" + r, v.wrappedHandler)
            };
            k = function(v, s, w) {
                var u = e(v), x;
                if (n[u] && n[u][s]) {
                    for (var t = 0, r = n[u][s].length; t < r; t++) {
                        x = n[u][s][t];
                        if (x && x.handler === w) {
                            v.detachEvent("on" + s, x.wrappedHandler);
                            n[u][s][t] = null
                        }
                    }
                }
            }
        } else {
            b = function(t, r, u) {
                var s = e(t);
                if (!g[s]) {
                    g[s] = {}
                }
                if (!g[s][r]) {
                    g[s][r] = [];
                    var v = t["on" + r];
                    if (v) {
                        g[s][r].push(v)
                    }
                    t["on" + r] = j(s, r)
                }
                g[s][r].push(u)
            };
            k = function(w, t, x) {
                var v = e(w);
                if (g[v] && g[v][t]) {
                    var s = g[v][t];
                    for (var u = 0, r = s.length; u < r; u++) {
                        if (s[u] === x) {
                            s.splice(u, 1)
                        }
                    }
                }
            }
        }
    }
    fabric.util.addListener = b;
    fabric.util.removeListener = k;
    function q(r) {
        return {x: a(r),y: p(r)}
    }
    function a(t) {
        var s = fabric.document.documentElement, r = fabric.document.body || {scrollLeft: 0};
        return t.pageX || ((typeof t.clientX != "unknown" ? t.clientX : 0) + (s.scrollLeft || r.scrollLeft) - (s.clientLeft || 0))
    }
    function p(t) {
        var s = fabric.document.documentElement, r = fabric.document.body || {scrollTop: 0};
        return t.pageY || ((typeof t.clientY != "unknown" ? t.clientY : 0) + (s.scrollTop || r.scrollTop) - (s.clientTop || 0))
    }
    if (fabric.isTouchSupported) {
        a = function(r) {
            return r.touches && r.touches[0] && r.touches[0].pageX
        };
        p = function(r) {
            return r.touches && r.touches[0] && r.touches[0].pageY
        }
    }
    fabric.util.getPointer = q;
    fabric.util.object.extend(fabric.util, fabric.Observable)
})(this);
(function() {
    function e(j, l) {
        var n = j.style, i;
        if (typeof l === "string") {
            j.style.cssText += ";" + l;
            return l.indexOf("opacity") > -1 ? c(j, l.match(/opacity:\s*(\d?\.?\d*)/)[1]) : j
        }
        for (var m in l) {
            if (m === "opacity") {
                c(j, l[m])
            } else {
                var k = (m === "float" || m === "cssFloat") ? (typeof n.styleFloat === "undefined" ? "cssFloat" : "styleFloat") : m;
                n[k] = l[m]
            }
        }
        return j
    }
    var h = fabric.document.createElement("div"), g = typeof h.style.opacity === "string", b = typeof h.style.filter === "string", a = fabric.document.defaultView, f = a && typeof a.getComputedStyle !== "undefined", d = /alpha\s*\(\s*opacity\s*=\s*([^\)]+)\)/, c = function(i) {
        return i
    };
    if (g) {
        c = function(i, j) {
            i.style.opacity = j;
            return i
        }
    } else {
        if (b) {
            c = function(i, j) {
                var k = i.style;
                if (i.currentStyle && !i.currentStyle.hasLayout) {
                    k.zoom = 1
                }
                if (d.test(k.filter)) {
                    j = j >= 0.9999 ? "" : ("alpha(opacity=" + (j * 100) + ")");
                    k.filter = k.filter.replace(d, j)
                } else {
                    k.filter += " alpha(opacity=" + (j * 100) + ")"
                }
                return i
            }
        }
    }
    fabric.util.setStyle = e
})();
(function() {
    var h = Array.prototype.slice;
    function g(j) {
        return typeof j === "string" ? fabric.document.getElementById(j) : j
    }
    function b(j) {
        return h.call(j, 0)
    }
    try {
        var i = b(fabric.document.childNodes) instanceof Array
    } catch (a) {
    }
    if (!i) {
        b = function(k) {
            var j = new Array(k.length), l = k.length;
            while (l--) {
                j[l] = k[l]
            }
            return j
        }
    }
    function c(k, j) {
        var l = fabric.document.createElement(k);
        for (var m in j) {
            if (m === "class") {
                l.className = j[m]
            } else {
                if (m === "for") {
                    l.htmlFor = j[m]
                } else {
                    l.setAttribute(m, j[m])
                }
            }
        }
        return l
    }
    function f(j, k) {
        if ((" " + j.className + " ").indexOf(" " + k + " ") === -1) {
            j.className += (j.className ? " " : "") + k
        }
    }
    function e(k, l, j) {
        if (typeof l === "string") {
            l = c(l, j)
        }
        if (k.parentNode) {
            k.parentNode.replaceChild(l, k)
        }
        l.appendChild(k);
        return l
    }
    function d(k) {
        var j = 0, l = 0;
        do {
            j += k.offsetTop || 0;
            l += k.offsetLeft || 0;
            k = k.offsetParent
        } while (k);
        return ({left: l,top: j})
    }
    (function() {
        var k = fabric.document.documentElement.style;
        var l = "userSelect" in k ? "userSelect" : "MozUserSelect" in k ? "MozUserSelect" : "WebkitUserSelect" in k ? "WebkitUserSelect" : "KhtmlUserSelect" in k ? "KhtmlUserSelect" : "";
        function m(n) {
            if (typeof n.onselectstart !== "undefined") {
                n.onselectstart = fabric.util.falseFunction
            }
            if (l) {
                n.style[l] = "none"
            } else {
                if (typeof n.unselectable == "string") {
                    n.unselectable = "on"
                }
            }
            return n
        }
        function j(n) {
            if (typeof n.onselectstart !== "undefined") {
                n.onselectstart = null
            }
            if (l) {
                n.style[l] = ""
            } else {
                if (typeof n.unselectable == "string") {
                    n.unselectable = ""
                }
            }
            return n
        }
        fabric.util.makeElementUnselectable = m;
        fabric.util.makeElementSelectable = j
    })();
    (function() {
        function j(k, o) {
            var m = fabric.document.getElementsByTagName("head")[0], l = fabric.document.createElement("script"), n = true;
            l.type = "text/javascript";
            l.setAttribute("runat", "server");
            l.onload = l.onreadystatechange = function(p) {
                if (n) {
                    if (typeof this.readyState == "string" && this.readyState !== "loaded" && this.readyState !== "complete") {
                        return
                    }
                    n = false;
                    o(p || fabric.window.event);
                    l = l.onload = l.onreadystatechange = null
                }
            };
            l.src = k;
            m.appendChild(l)
        }
        fabric.util.getScript = j
    })();
    fabric.util.getById = g;
    fabric.util.toArray = b;
    fabric.util.makeElement = c;
    fabric.util.addClass = f;
    fabric.util.wrapElement = e;
    fabric.util.getElementOffset = d
})();
(function() {
    function d(e, f) {
        return e + (/\?/.test(e) ? "&" : "?") + f
    }
    var c = (function() {
        var h = [function() {
                return new ActiveXObject("Microsoft.XMLHTTP")
            }, function() {
                return new ActiveXObject("Msxml2.XMLHTTP")
            }, function() {
                return new ActiveXObject("Msxml2.XMLHTTP.3.0")
            }, function() {
                return new XMLHttpRequest()
            }];
        for (var e = h.length; e--; ) {
            try {
                var g = h[e]();
                if (g) {
                    return h[e]
                }
            } catch (f) {
            }
        }
    })();
    function a() {
    }
    function b(g, f) {
        f || (f = {});
        var j = f.method ? f.method.toUpperCase() : "GET", i = f.onComplete || function() {
        }, h = c(), e;
        h.onreadystatechange = function() {
            if (h.readyState === 4) {
                i(h);
                h.onreadystatechange = a
            }
        };
        if (j === "GET") {
            e = null;
            if (typeof f.parameters == "string") {
                g = d(g, f.parameters)
            }
        }
        h.open(j, g, true);
        if (j === "POST" || j === "PUT") {
            h.setRequestHeader("Content-Type", "application/x-www-form-urlencoded")
        }
        h.send(e);
        return h
    }
    fabric.util.request = b
})();
(function(i) {
    var g = i.fabric || (i.fabric = {}), o = g.util.object.extend, e = g.util.string.capitalize, p = g.util.object.clone;
    var k = {cx: "left",x: "left",cy: "top",y: "top",r: "radius","fill-opacity": "opacity","fill-rule": "fillRule","stroke-width": "strokeWidth",transform: "transformMatrix"};
    function d(v, u) {
        if (!v) {
            return
        }
        var x, t, s = {};
        if (v.parentNode && /^g$/i.test(v.parentNode.nodeName)) {
            s = g.parseAttributes(v.parentNode, u)
        }
        var w = u.reduce(function(z, y) {
            x = v.getAttribute(y);
            t = parseFloat(x);
            if (x) {
                if ((y === "fill" || y === "stroke") && x === "none") {
                    x = ""
                }
                if (y === "fill-rule") {
                    x = (x === "evenodd") ? "destination-over" : x
                }
                if (y === "transform") {
                    x = g.parseTransformAttribute(x)
                }
                if (y in k) {
                    y = k[y]
                }
                z[y] = isNaN(t) ? x : t
            }
            return z
        }, {});
        w = o(w, o(n(v), g.parseStyleAttribute(v)));
        return o(s, w)
    }
    g.parseTransformAttribute = (function() {
        function s(L, M) {
            var N = M[0];
            L[0] = Math.cos(N);
            L[1] = Math.sin(N);
            L[2] = -Math.sin(N);
            L[3] = Math.cos(N)
        }
        function x(N, O) {
            var M = O[0], L = (O.length === 2) ? O[1] : O[0];
            N[0] = M;
            N[3] = L
        }
        function I(L, M) {
            L[2] = M[0]
        }
        function u(L, M) {
            L[1] = M[0]
        }
        function F(L, M) {
            L[4] = M[0];
            if (M.length === 2) {
                L[5] = M[1]
            }
        }
        var z = [1, 0, 0, 1, 0, 0], t = "(?:[-+]?\\d+(?:\\.\\d+)?(?:e[-+]?\\d+)?)", J = "(?:\\s+,?\\s*|,\\s*)", A = "(?:(skewX)\\s*\\(\\s*(" + t + ")\\s*\\))", y = "(?:(skewY)\\s*\\(\\s*(" + t + ")\\s*\\))", H = "(?:(rotate)\\s*\\(\\s*(" + t + ")(?:" + J + "(" + t + ")" + J + "(" + t + "))?\\s*\\))", K = "(?:(scale)\\s*\\(\\s*(" + t + ")(?:" + J + "(" + t + "))?\\s*\\))", D = "(?:(translate)\\s*\\(\\s*(" + t + ")(?:" + J + "(" + t + "))?\\s*\\))", G = "(?:(matrix)\\s*\\(\\s*(" + t + ")" + J + "(" + t + ")" + J + "(" + t + ")" + J + "(" + t + ")" + J + "(" + t + ")" + J + "(" + t + ")\\s*\\))", E = "(?:" + G + "|" + D + "|" + K + "|" + H + "|" + A + "|" + y + ")", B = "(?:" + E + "(?:" + J + E + ")*)", v = "^\\s*(?:" + B + "?)\\s*$", C = new RegExp(v), w = new RegExp(E);
        return function(M) {
            var L = z.concat();
            if (!M || (M && !C.test(M))) {
                return L
            }
            M.replace(w, function(Q) {
                var N = new RegExp(E).exec(Q).filter(function(R) {
                    return (R !== "" && R != null)
                }), O = N[1], P = N.slice(2).map(parseFloat);
                switch (O) {
                    case "translate":
                        F(L, P);
                        break;
                    case "rotate":
                        s(L, P);
                        break;
                    case "scale":
                        x(L, P);
                        break;
                    case "skewX":
                        I(L, P);
                        break;
                    case "skewY":
                        u(L, P);
                        break;
                    case "matrix":
                        L = P;
                        break
                }
            });
            return L
        }
    })();
    function r(v) {
        if (!v) {
            return null
        }
        v = v.trim();
        var x = v.indexOf(",") > -1;
        v = v.split(/\s+/);
        var t = [];
        if (x) {
            for (var u = 0, s = v.length; u < s; u++) {
                var w = v[u].split(",");
                t.push({x: parseFloat(w[0]),y: parseFloat(w[1])})
            }
        } else {
            for (var u = 0, s = v.length; u < s; u += 2) {
                t.push({x: parseFloat(v[u]),y: parseFloat(v[u + 1])})
            }
        }
        if (t.length % 2 !== 0) {
        }
        return t
    }
    function h(t) {
        var s = {}, u = t.getAttribute("style");
        if (u) {
            if (typeof u == "string") {
                u = u.replace(/;$/, "").split(";").forEach(function(x) {
                    var w = x.split(":");
                    s[w[0].trim().toLowerCase()] = w[1].trim()
                })
            } else {
                for (var v in u) {
                    if (typeof u[v] !== "undefined") {
                        s[v.toLowerCase()] = u[v]
                    }
                }
            }
        }
        return s
    }
    function q(x) {
        var t = g.Canvas.activeInstance, s = t ? t.getContext() : null;
        if (!s) {
            return
        }
        for (var u = x.length; u--; ) {
            var v = x[u].get("fill");
            if (/^url\(/.test(v)) {
                var w = v.slice(5, v.length - 1);
                if (g.gradientDefs[w]) {
                    x[u].set("fill", g.Gradient.fromElement(g.gradientDefs[w], s, x[u]))
                }
            }
        }
    }
    function f(t, B, C) {
        var s = Array(t.length), w = t.length;
        function u() {
            if (--w === 0) {
                s = s.filter(function(D) {
                    return D != null
                });
                q(s);
                B(s)
            }
        }
        for (var y = 0, v, x = t.length; y < x; y++) {
            v = t[y];
            var A = g[e(v.tagName)];
            if (A && A.fromElement) {
                try {
                    if (A.async) {
                        A.fromElement(v, (function(D) {
                            return function(E) {
                                s.splice(D, 0, E);
                                u()
                            }
                        })(y), C)
                    } else {
                        s.splice(y, 0, A.fromElement(v, C));
                        u()
                    }
                } catch (z) {
                    g.log(z.message || z)
                }
            } else {
                u()
            }
        }
    }
    function c(x) {
        var v = x.getElementsByTagName("style"), t = {}, y;
        for (var u = 0, s = v.length; u < s; u++) {
            var w = v[0].textContent;
            w = w.replace(/\/\*[\s\S]*?\*\//g, "");
            y = w.match(/[^{]*\{[\s\S]*?\}/g);
            y = y.map(function(z) {
                return z.trim()
            });
            y.forEach(function(E) {
                var C = E.match(/([\s\S]*?)\s*\{([^}]*)\}/), E = C[1], z = C[2].trim(), G = z.replace(/;$/, "").split(/\s*;\s*/);
                if (!t[E]) {
                    t[E] = {}
                }
                for (var B = 0, D = G.length; B < D; B++) {
                    var A = G[B].split(/\s*:\s*/), H = A[0], F = A[1];
                    t[E][H] = F
                }
            })
        }
        return t
    }
    function n(t) {
        var z = t.nodeName, u = t.getAttribute("class"), y = t.getAttribute("id"), v = {};
        for (var x in g.cssRules) {
            var s = (u && new RegExp("^\\." + u).test(x)) || (y && new RegExp("^#" + y).test(x)) || (new RegExp("^" + z).test(x));
            if (s) {
                for (var w in g.cssRules[x]) {
                    v[w] = g.cssRules[x][w]
                }
            }
        }
        return v
    }
    g.parseSVGDocument = (function() {
        var s = /^(path|circle|polygon|polyline|ellipse|rect|line|image)$/;
        var t = "(?:[-+]?\\d+(?:\\.\\d+)?(?:e[-+]?\\d+)?)";
        var u = new RegExp("^\\s*(" + t + "+)\\s*,?\\s*(" + t + "+)\\s*,?\\s*(" + t + "+)\\s*,?\\s*(" + t + "+)\\s*$");
        function v(w, x) {
            while (w && (w = w.parentNode)) {
                if (x.test(w.nodeName)) {
                    return true
                }
            }
            return false
        }
        return function(I, K) {
            if (!I) {
                return
            }
            var A = new Date(), E = g.util.toArray(I.getElementsByTagName("*"));
            if (E.length === 0) {
                E = I.selectNodes("//*[name(.)!='svg']");
                var G = [];
                for (var F = 0, H = E.length; F < H; F++) {
                    G[F] = E[F]
                }
                E = G
            }
            var w = E.filter(function(M) {
                return s.test(M.tagName) && !v(M, /^(?:pattern|defs)$/)
            });
            if (!w || (w && !w.length)) {
                return
            }
            var x = I.getAttribute("viewBox"), C = I.getAttribute("width"), z = I.getAttribute("height"), y = null, J = null, D, B;
            if (x && (x = x.match(u))) {
                D = parseInt(x[1], 10);
                B = parseInt(x[2], 10);
                y = parseInt(x[3], 10);
                J = parseInt(x[4], 10)
            }
            y = C ? parseFloat(C) : y;
            J = z ? parseFloat(z) : J;
            var L = {width: y,height: J};
            g.gradientDefs = g.getGradientDefs(I);
            g.cssRules = c(I);
            g.parseElements(w, function(M) {
                g.documentParsingTime = new Date() - A;
                if (K) {
                    K(M, L)
                }
            }, p(L))
        }
    })();
    var j = {has: function(s, t) {
            t(false)
        },get: function(s, t) {
        },set: function(t, s) {
        }};
    function b(s, u) {
        s = s.replace(/^\n\s*/, "").trim();
        j.has(s, function(v) {
            if (v) {
                j.get(s, function(x) {
                    var w = a(x);
                    u(w.objects, w.options)
                })
            } else {
                new g.util.request(s, {method: "get",onComplete: t})
            }
        });
        function t(w) {
            var v = w.responseXML;
            if (!v.documentElement && g.window.ActiveXObject && w.responseText) {
                v = new ActiveXObject("Microsoft.XMLDOM");
                v.async = "false";
                v.loadXML(w.responseText.replace(/<!DOCTYPE[\s\S]*?(\[[\s\S]*\])*?>/i, ""))
            }
            if (!v.documentElement) {
                return
            }
            g.parseSVGDocument(v.documentElement, function(y, x) {
                j.set(s, {objects: g.util.array.invoke(y, "toObject"),options: x});
                u(y, x)
            })
        }
    }
    function a(u) {
        var t = u.objects, s = u.options;
        t = t.map(function(v) {
            return g[e(v.type)].fromObject(v)
        });
        return ({objects: t,options: s})
    }
    function m(s, v) {
        s = s.trim();
        var t;
        if (typeof DOMParser !== "undefined") {
            var u = new DOMParser();
            if (u && u.parseFromString) {
                t = u.parseFromString(s, "text/xml")
            }
        } else {
            if (g.window.ActiveXObject) {
                var t = new ActiveXObject("Microsoft.XMLDOM");
                t.async = "false";
                t.loadXML(s.replace(/<!DOCTYPE[\s\S]*?(\[[\s\S]*\])*?>/i, ""))
            }
        }
        g.parseSVGDocument(t.documentElement, function(x, w) {
            v(x, w)
        })
    }
    function l(v) {
        var t = "";
        for (var u = 0, s = v.length; u < s; u++) {
            if (v[u].type !== "text" || !v[u].path) {
                continue
            }
            t += ["@font-face {", "font-family: ", v[u].fontFamily, "; ", "src: url('", v[u].path, "')", "}"].join("")
        }
        if (t) {
            t = ["<defs>", '<style type="text/css">', "<![CDATA[", t, "]]>", "</style>", "</defs>"].join("")
        }
        return t
    }
    o(g, {parseAttributes: d,parseElements: f,parseStyleAttribute: h,parsePointsAttribute: r,getCSSRules: c,loadSVGFromURL: b,loadSVGFromString: m,createSVGFontFacesMarkup: l})
})(typeof exports != "undefined" ? exports : this);
(function() {
    function c(h) {
        var g = h.getAttribute("style");
        if (g) {
            var k = g.split(/\s*;\s*/);
            for (var f = k.length; f--; ) {
                var e = k[f].split(/\s*:\s*/), d = e[0].trim(), j = e[1].trim();
                if (d === "stop-color") {
                    return j
                }
            }
        }
    }
    fabric.Gradient = {create: function(l, m) {
            m || (m = {});
            var f = m.x1 || 0, k = m.y1 || 0, e = m.x2 || l.canvas.width, i = m.y2 || 0, g = m.colorStops;
            var j = l.createLinearGradient(f, k, e, i);
            for (var h in g) {
                var d = g[h];
                j.addColorStop(parseFloat(h), d)
            }
            return j
        },fromElement: function(d, m, l) {
            var j = d.getElementsByTagName("stop"), d, f, e = {}, g, k = {x1: d.getAttribute("x1") || 0,y1: d.getAttribute("y1") || 0,x2: d.getAttribute("x2") || "100%",y2: d.getAttribute("y2") || 0};
            for (var h = j.length; h--; ) {
                d = j[h];
                f = d.getAttribute("offset");
                f = parseFloat(f) / (/%$/.test(f) ? 100 : 1);
                e[f] = c(d) || d.getAttribute("stop-color")
            }
            a(l, k);
            return fabric.Gradient.create(m, {x1: k.x1,y1: k.y1,x2: k.x2,y2: k.y2,colorStops: e})
        },forObject: function(g, d, e) {
            e || (e = {});
            a(g, e);
            var f = fabric.Gradient.create(d, {x1: e.x1,y1: e.y1,x2: e.x2,y2: e.y2,colorStops: e.colorStops});
            return f
        }};
    function a(f, e) {
        for (var g in e) {
            if (typeof e[g] === "string" && /^\d+%$/.test(e[g])) {
                var d = parseFloat(e[g], 10);
                if (g === "x1" || g === "x2") {
                    e[g] = f.width * d / 100
                } else {
                    if (g === "y1" || g === "y2") {
                        e[g] = f.height * d / 100
                    }
                }
            }
            if (g === "x1" || g === "x2") {
                e[g] -= f.width / 2
            } else {
                if (g === "y1" || g === "y2") {
                    e[g] -= f.height / 2
                }
            }
        }
    }
    function b(j) {
        var f = j.getElementsByTagName("linearGradient"), d = j.getElementsByTagName("radialGradient"), g, h = {};
        for (var e = f.length; e--; ) {
            g = f[e];
            h[g.id] = g
        }
        for (var e = d.length; e--; ) {
            g = d[e];
            h[g.id] = g
        }
        return h
    }
    fabric.getGradientDefs = b
})();
(function(b) {
    var c = b.fabric || (b.fabric = {});
    if (c.Point) {
        c.warn("fabric.Point is already defined");
        return
    }
    c.Point = a;
    function a(d, e) {
        if (arguments.length > 0) {
            this.init(d, e)
        }
    }
    a.prototype = {constructor: a,init: function(d, e) {
            this.x = d;
            this.y = e
        },add: function(d) {
            return new a(this.x + d.x, this.y + d.y)
        },addEquals: function(d) {
            this.x += d.x;
            this.y += d.y;
            return this
        },scalarAdd: function(d) {
            return new a(this.x + d, this.y + d)
        },scalarAddEquals: function(d) {
            this.x += d;
            this.y += d;
            return this
        },subtract: function(d) {
            return new a(this.x - d.x, this.y - d.y)
        },subtractEquals: function(d) {
            this.x -= d.x;
            this.y -= d.y;
            return this
        },scalarSubtract: function(d) {
            return new a(this.x - d, this.y - d)
        },scalarSubtractEquals: function(d) {
            this.x -= d;
            this.y -= d;
            return this
        },multiply: function(d) {
            return new a(this.x * d, this.y * d)
        },multiplyEquals: function(d) {
            this.x *= d;
            this.y *= d;
            return this
        },divide: function(d) {
            return new a(this.x / d, this.y / d)
        },divideEquals: function(d) {
            this.x /= d;
            this.y /= d;
            return this
        },eq: function(d) {
            return (this.x == d.x && this.y == d.y)
        },lt: function(d) {
            return (this.x < d.x && this.y < d.y)
        },lte: function(d) {
            return (this.x <= d.x && this.y <= d.y)
        },gt: function(d) {
            return (this.x > d.x && this.y > d.y)
        },gte: function(d) {
            return (this.x >= d.x && this.y >= d.y)
        },lerp: function(e, d) {
            return new a(this.x + (e.x - this.x) * d, this.y + (e.y - this.y) * d)
        },distanceFrom: function(f) {
            var e = this.x - f.x, d = this.y - f.y;
            return Math.sqrt(e * e + d * d)
        },min: function(d) {
            return new a(Math.min(this.x, d.x), Math.min(this.y, d.y))
        },max: function(d) {
            return new a(Math.max(this.x, d.x), Math.max(this.y, d.y))
        },toString: function() {
            return this.x + "," + this.y
        },setXY: function(d, e) {
            this.x = d;
            this.y = e
        },setFromPoint: function(d) {
            this.x = d.x;
            this.y = d.y
        },swap: function(e) {
            var d = this.x, f = this.y;
            this.x = e.x;
            this.y = e.y;
            e.x = d;
            e.y = f
        }}
})(typeof exports != "undefined" ? exports : this);
(function(a) {
    var b = a.fabric || (a.fabric = {});
    if (b.Intersection) {
        b.warn("fabric.Intersection is already defined");
        return
    }
    function c(d) {
        if (arguments.length > 0) {
            this.init(d)
        }
    }
    b.Intersection = c;
    b.Intersection.prototype = {init: function(d) {
            this.status = d;
            this.points = []
        },appendPoint: function(d) {
            this.points.push(d)
        },appendPoints: function(d) {
            this.points = this.points.concat(d)
        }};
    b.Intersection.intersectLineLine = function(h, f, l, k) {
        var m, i = (k.x - l.x) * (h.y - l.y) - (k.y - l.y) * (h.x - l.x), j = (f.x - h.x) * (h.y - l.y) - (f.y - h.y) * (h.x - l.x), g = (k.y - l.y) * (f.x - h.x) - (k.x - l.x) * (f.y - h.y);
        if (g != 0) {
            var e = i / g, d = j / g;
            if (0 <= e && e <= 1 && 0 <= d && d <= 1) {
                m = new c("Intersection");
                m.points.push(new b.Point(h.x + e * (f.x - h.x), h.y + e * (f.y - h.y)))
            } else {
                m = new c("No Intersection")
            }
        } else {
            if (i == 0 || j == 0) {
                m = new c("Coincident")
            } else {
                m = new c("Parallel")
            }
        }
        return m
    };
    b.Intersection.intersectLinePolygon = function(e, d, l) {
        var m = new c("No Intersection"), f = l.length;
        for (var h = 0; h < f; h++) {
            var k = l[h], j = l[(h + 1) % f], g = c.intersectLineLine(e, d, k, j);
            m.appendPoints(g.points)
        }
        if (m.points.length > 0) {
            m.status = "Intersection"
        }
        return m
    };
    b.Intersection.intersectPolygonPolygon = function(j, h) {
        var f = new c("No Intersection"), l = j.length;
        for (var k = 0; k < l; k++) {
            var g = j[k], e = j[(k + 1) % l], d = c.intersectLinePolygon(g, e, h);
            f.appendPoints(d.points)
        }
        if (f.points.length > 0) {
            f.status = "Intersection"
        }
        return f
    };
    b.Intersection.intersectPolygonRectangle = function(n, e, d) {
        var g = e.min(d), m = e.max(d), f = new b.Point(m.x, g.y), l = new b.Point(g.x, m.y), k = c.intersectLinePolygon(g, f, n), j = c.intersectLinePolygon(f, m, n), i = c.intersectLinePolygon(m, l, n), h = c.intersectLinePolygon(l, g, n), o = new c("No Intersection");
        o.appendPoints(k.points);
        o.appendPoints(j.points);
        o.appendPoints(i.points);
        o.appendPoints(h.points);
        if (o.points.length > 0) {
            o.status = "Intersection"
        }
        return o
    }
})(typeof exports != "undefined" ? exports : this);
(function(a) {
    var b = a.fabric || (a.fabric = {});
    if (b.Color) {
        b.warn("fabric.Color is already defined.");
        return
    }
    function c(d) {
        if (!d) {
            this.setSource([0, 0, 0, 1])
        } else {
            this._tryParsingColor(d)
        }
    }
    b.Color = c;
    b.Color.prototype = {_tryParsingColor: function(d) {
            var e = c.sourceFromHex(d);
            if (!e) {
                e = c.sourceFromRgb(d)
            }
            if (e) {
                this.setSource(e)
            }
        },getSource: function() {
            return this._source
        },setSource: function(d) {
            this._source = d
        },toRgb: function() {
            var d = this.getSource();
            return "rgb(" + d[0] + "," + d[1] + "," + d[2] + ")"
        },toRgba: function() {
            var d = this.getSource();
            return "rgba(" + d[0] + "," + d[1] + "," + d[2] + "," + d[3] + ")"
        },toHex: function() {
            var h = this.getSource();
            var f = h[0].toString(16);
            f = (f.length == 1) ? ("0" + f) : f;
            var e = h[1].toString(16);
            e = (e.length == 1) ? ("0" + e) : e;
            var d = h[2].toString(16);
            d = (d.length == 1) ? ("0" + d) : d;
            return f.toUpperCase() + e.toUpperCase() + d.toUpperCase()
        },getAlpha: function() {
            return this.getSource()[3]
        },setAlpha: function(e) {
            var d = this.getSource();
            d[3] = e;
            this.setSource(d);
            return this
        },toGrayscale: function() {
            var f = this.getSource(), e = parseInt((f[0] * 0.3 + f[1] * 0.59 + f[2] * 0.11).toFixed(0), 10), d = f[3];
            this.setSource([e, e, e, d]);
            return this
        },toBlackWhite: function(d) {
            var g = this.getSource(), f = (g[0] * 0.3 + g[1] * 0.59 + g[2] * 0.11).toFixed(0), e = g[3], d = d || 127;
            f = (Number(f) < Number(d)) ? 0 : 255;
            this.setSource([f, f, f, e]);
            return this
        },overlayWith: function(k) {
            if (!(k instanceof c)) {
                k = new c(k)
            }
            var d = [], j = this.getAlpha(), g = 0.5, h = this.getSource(), e = k.getSource();
            for (var f = 0; f < 3; f++) {
                d.push(Math.round((h[f] * (1 - g)) + (e[f] * g)))
            }
            d[3] = j;
            this.setSource(d);
            return this
        }};
    b.Color.reRGBa = /^rgba?\((\d{1,3})\s*,\s*(\d{1,3})\s*,\s*(\d{1,3})(?:\s*,\s*(\d+(?:\.\d+)?))?\)$/;
    b.Color.reHex = /^#?([0-9a-f]{6}|[0-9a-f]{3})$/i;
    b.Color.fromRgb = function(d) {
        return c.fromSource(c.sourceFromRgb(d))
    };
    b.Color.sourceFromRgb = function(d) {
        var e = d.match(c.reRGBa);
        if (e) {
            return [parseInt(e[1], 10), parseInt(e[2], 10), parseInt(e[3], 10), e[4] ? parseFloat(e[4]) : 1]
        }
    };
    b.Color.fromRgba = c.fromRgb;
    b.Color.fromHex = function(d) {
        return c.fromSource(c.sourceFromHex(d))
    };
    b.Color.sourceFromHex = function(f) {
        if (f.match(c.reHex)) {
            var j = f.slice(f.indexOf("#") + 1), e = (j.length === 3), i = e ? (j.charAt(0) + j.charAt(0)) : j.substring(0, 2), h = e ? (j.charAt(1) + j.charAt(1)) : j.substring(2, 4), d = e ? (j.charAt(2) + j.charAt(2)) : j.substring(4, 6);
            return [parseInt(i, 16), parseInt(h, 16), parseInt(d, 16), 1]
        }
    };
    b.Color.fromSource = function(d) {
        var e = new c();
        e.setSource(d);
        return e
    }
})(typeof exports != "undefined" ? exports : this);
(function(c) {
    if (fabric.StaticCanvas) {
        fabric.warn("fabric.StaticCanvas is already defined.");
        return
    }
    var f = fabric.util.object.extend, d = fabric.util.getElementOffset, e = fabric.util.removeFromArray, b = fabric.util.removeListener, a = new Error("Could not initialize `canvas` element");
    fabric.StaticCanvas = function(h, g) {
        g || (g = {});
        this._initStatic(h, g);
        fabric.StaticCanvas.activeInstance = this
    };
    f(fabric.StaticCanvas.prototype, fabric.Observable);
    f(fabric.StaticCanvas.prototype, {backgroundColor: "rgba(0, 0, 0, 0)",backgroundImage: "",backgroundImageOpacity: 1,backgroundImageStretch: true,includeDefaultValues: true,stateful: true,renderOnAddition: true,clipTo: null,CANVAS_WIDTH: 600,CANVAS_HEIGHT: 600,onBeforeScaleRotate: function(g) {
        },onFpsUpdate: null,_initStatic: function(h, g) {
            this._objects = [];
            this._createLowerCanvas(h);
            this._initOptions(g);
            if (g.overlayImage) {
                this.setOverlayImage(g.overlayImage, this.renderAll.bind(this))
            }
            if (g.backgroundImage) {
                this.setBackgroundImage(g.backgroundImage, this.renderAll.bind(this))
            }
            this.calcOffset()
        },calcOffset: function() {
            this._offset = d(this.lowerCanvasEl);
            return this
        },setOverlayImage: function(g, h) {
            return fabric.util.loadImage(g, function(i) {
                this.overlayImage = i;
                h && h()
            }, this)
        },setBackgroundImage: function(h, i, g) {
            return fabric.util.loadImage(h, function(j) {
                this.backgroundImage = j;
                if (g && g.backgroundOpacity) {
                    this.backgroundOpacity = g.backgroundOpacity
                }
                if (g && g.backgroundStretch) {
                    this.backgroundStretch = g.backgroundStretch
                }
                i && i()
            }, this)
        },_createCanvasElement: function() {
            var g = fabric.document.createElement("canvas");
            if (!g.style) {
                g.style = {}
            }
            if (!g) {
                throw a
            }
            this._initCanvasElement(g);
            return g
        },_initCanvasElement: function(g) {
            if (typeof g.getContext === "undefined" && typeof G_vmlCanvasManager !== "undefined" && G_vmlCanvasManager.initElement) {
                G_vmlCanvasManager.initElement(g)
            }
            if (typeof g.getContext === "undefined") {
                throw a
            }
        },_initOptions: function(g) {
            for (var h in g) {
                this[h] = g[h]
            }
            this.width = parseInt(this.lowerCanvasEl.width, 10) || 0;
            this.height = parseInt(this.lowerCanvasEl.height, 10) || 0;
            this.lowerCanvasEl.style.width = this.width + "px";
            this.lowerCanvasEl.style.height = this.height + "px"
        },_createLowerCanvas: function(g) {
            this.lowerCanvasEl = fabric.util.getById(g) || this._createCanvasElement();
            this._initCanvasElement(this.lowerCanvasEl);
            fabric.util.addClass(this.lowerCanvasEl, "lower-canvas");
            if (this.interactive) {
                this._applyCanvasStyle(this.lowerCanvasEl)
            }
            this.contextContainer = this.lowerCanvasEl.getContext("2d")
        },getWidth: function() {
            return this.width
        },getHeight: function() {
            return this.height
        },setWidth: function(g) {
            return this._setDimension("width", g)
        },setHeight: function(g) {
            return this._setDimension("height", g)
        },setDimensions: function(g) {
            for (var h in g) {
                this._setDimension(h, g[h])
            }
            return this
        },_setDimension: function(h, g) {
            this.lowerCanvasEl[h] = g;
            this.lowerCanvasEl.style[h] = g + "px";
            if (this.upperCanvasEl) {
                this.upperCanvasEl[h] = g;
                this.upperCanvasEl.style[h] = g + "px"
            }
            if (this.wrapperEl) {
                this.wrapperEl.style[h] = g + "px"
            }
            this[h] = g;
            this.calcOffset();
            this.renderAll();
            return this
        },getElement: function() {
            return this.lowerCanvasEl
        },getActiveObject: function() {
            return null
        },getActiveGroup: function() {
            return null
        },_draw: function(g, h) {
            h && h.render(g)
        },add: function() {
            this._objects.push.apply(this._objects, arguments);
            for (var g = arguments.length; g--; ) {
                this.stateful && arguments[g].setupState();
                arguments[g].setCoords()
            }
            this.renderOnAddition && this.renderAll();
            return this
        },insertAt: function(h, g, i) {
            if (i) {
                this._objects[g] = h
            } else {
                this._objects.splice(g, 0, h)
            }
            this.stateful && h.setupState();
            h.setCoords();
            this.renderAll();
            return this
        },getObjects: function() {
            return this._objects
        },clearContext: function(g) {
            g.clearRect(0, 0, this.width, this.height);
            return this
        },clear: function() {
            this._objects.length = 0;
            this.clearContext(this.contextContainer);
            if (this.contextTop) {
                this.clearContext(this.contextTop)
            }
            this.renderAll();
            return this
        },renderAll: function(h) {
            var j = this[(h === true && this.interactive) ? "contextTop" : "contextContainer"];
            if (this.contextTop) {
                this.clearContext(this.contextTop)
            }
            if (h === false || (typeof h === "undefined")) {
                this.clearContext(j)
            }
            var n = this._objects.length, m = this.getActiveGroup(), l = new Date();
            if (this.clipTo) {
                j.save();
                j.beginPath();
                this.clipTo(j);
                j.clip()
            }
            j.fillStyle = this.backgroundColor;
            j.fillRect(0, 0, this.width, this.height);
            if (typeof this.backgroundImage == "object") {
                j.save();
                j.globalAlpha = this.backgroundImageOpacity;
                if (this.backgroundImageStretch) {
                    j.drawImage(this.backgroundImage, 0, 0, this.width, this.height)
                } else {
                    j.drawImage(this.backgroundImage, 0, 0)
                }
                j.restore()
            }
            if (n) {
                for (var k = 0; k < n; ++k) {
                    if (!m || (m && this._objects[k] && !m.contains(this._objects[k]))) {
                        this._draw(j, this._objects[k])
                    }
                }
            }
            if (this.clipTo) {
                j.restore()
            }
            if (m) {
                this._draw(this.contextTop, m)
            }
            if (this.overlayImage) {
                (this.contextTop || this.contextContainer).drawImage(this.overlayImage, 0, 0)
            }
            if (this.onFpsUpdate) {
                var g = new Date() - l;
                this.onFpsUpdate(~~(1000 / g))
            }
            this.fire("after:render");
            return this
        },renderTop: function() {
            this.clearContext(this.contextTop || this.contextContainer);
            if (this.overlayImage) {
                (this.contextTop || this.contextContainer).drawImage(this.overlayImage, 0, 0)
            }
            if (this.selection && this._groupSelector) {
                this._drawSelection()
            }
            var g = this.getActiveGroup();
            if (g) {
                g.render(this.contextTop)
            }
            this.fire("after:render");
            return this
        },toDataURL: function(h) {
            this.renderAll(true);
            var g = (this.upperCanvasEl || this.lowerCanvasEl).toDataURL("image/" + h);
            this.renderAll();
            return g
        },toDataURLWithMultiplier: function(j, n) {
            var m = this.getWidth(), l = this.getHeight(), h = m * n, i = l * n, g = this.getActiveObject();
            this.setWidth(h).setHeight(i);
            this.contextTop.scale(n, n);
            if (g) {
                this.deactivateAll()
            }
            this.width = m;
            this.height = l;
            this.renderAll(true);
            var k = this.toDataURL(j);
            this.contextTop.scale(1 / n, 1 / n);
            this.setWidth(m).setHeight(l);
            if (g) {
                this.setActiveObject(g)
            }
            this.renderAll();
            return k
        },getCenter: function() {
            return {top: this.getHeight() / 2,left: this.getWidth() / 2}
        },centerObjectH: function(g) {
            g.set("left", this.getCenter().left);
            this.renderAll();
            return this
        },centerObjectV: function(g) {
            g.set("top", this.getCenter().top);
            this.renderAll();
            return this
        },centerObject: function(g) {
            return this.centerObjectH(g).centerObjectV(g)
        },straightenObject: function(g) {
            g.straighten();
            this.renderAll();
            return this
        },toDatalessJSON: function() {
            return this.toDatalessObject()
        },toObject: function() {
            return this._toObjectMethod("toObject")
        },toDatalessObject: function() {
            return this._toObjectMethod("toDatalessObject")
        },_toObjectMethod: function(g) {
            return {objects: this._objects.map(function(h) {
                    if (!this.includeDefaultValues) {
                        var i = h.includeDefaultValues;
                        h.includeDefaultValues = false
                    }
                    var j = h[g]();
                    if (!this.includeDefaultValues) {
                        h.includeDefaultValues = i
                    }
                    return j
                }, this),background: this.backgroundColor}
        },toSVG: function() {
            var h = ['<?xml version="1.0" standalone="no" ?>', '<!DOCTYPE svg PUBLIC "-//W3C//DTD SVG 20010904//EN" ', '"http://www.w3.org/TR/2001/REC-SVG-20010904/DTD/svg10.dtd">', "<svg ", 'xmlns="http://www.w3.org/2000/svg" ', 'xmlns:xlink="http://www.w3.org/1999/xlink" ', 'version="1.1" ', 'width="', this.width, '" ', 'height="', this.height, '" ', 'xml:space="preserve">', "<desc>Created with Fabric.js ", fabric.version, "</desc>", fabric.createSVGFontFacesMarkup(this.getObjects())];
            for (var j = 0, k = this.getObjects(), g = k.length; j < g; j++) {
                h.push(k[j].toSVG())
            }
            h.push("</svg>");
            return h.join("")
        },isEmpty: function() {
            return this._objects.length === 0
        },remove: function(g) {
            e(this._objects, g);
            if (this.getActiveObject() === g) {
                this.discardActiveObject()
            }
            this.renderAll();
            return g
        },sendToBack: function(g) {
            e(this._objects, g);
            this._objects.unshift(g);
            return this.renderAll()
        },bringToFront: function(g) {
            e(this._objects, g);
            this._objects.push(g);
            return this.renderAll()
        },sendBackwards: function(j) {
            var h = this._objects.indexOf(j), g = h;
            if (h !== 0) {
                for (var k = h - 1; k >= 0; --k) {
                    if (j.intersectsWithObject(this._objects[k]) || j.isContainedWithinObject(this._objects[k])) {
                        g = k;
                        break
                    }
                }
                e(this._objects, j);
                this._objects.splice(g, 0, j)
            }
            return this.renderAll()
        },bringForward: function(k) {
            var n = this.getObjects(), h = n.indexOf(k), g = h;
            if (h !== n.length - 1) {
                for (var m = h + 1, j = this._objects.length; m < j; ++m) {
                    if (k.intersectsWithObject(n[m]) || k.isContainedWithinObject(this._objects[m])) {
                        g = m;
                        break
                    }
                }
                e(n, k);
                n.splice(g, 0, k)
            }
            this.renderAll()
        },item: function(g) {
            return this.getObjects()[g]
        },complexity: function() {
            return this.getObjects().reduce(function(g, h) {
                g += h.complexity ? h.complexity() : 0;
                return g
            }, 0)
        },forEachObject: function(k, h) {
            var j = this.getObjects(), g = j.length;
            while (g--) {
                k.call(h, j[g], g, j)
            }
            return this
        },dispose: function() {
            this.clear();
            if (this.interactive) {
                b(this.upperCanvasEl, "mousedown", this._onMouseDown);
                b(this.upperCanvasEl, "mousemove", this._onMouseMove);
                b(fabric.window, "resize", this._onResize)
            }
            return this
        },_resizeImageToFit: function(i) {
            var h = i.width || i.offsetWidth, g = this.getWidth() / h;
            if (h) {
                i.width = h * g
            }
        }});
    fabric.StaticCanvas.prototype.toString = function() {
        return "#<fabric.Canvas (" + this.complexity() + "): { objects: " + this.getObjects().length + " }>"
    };
    f(fabric.StaticCanvas, {EMPTY_JSON: '{"objects": [], "background": "white"}',toGrayscale: function(l) {
            var k = l.getContext("2d"), g = k.getImageData(0, 0, l.width, l.height), p = g.data, m = g.width, r = g.height, q, h, o, n;
            for (o = 0; o < m; o++) {
                for (n = 0; n < r; n++) {
                    q = (o * 4) * r + (n * 4);
                    h = (p[q] + p[q + 1] + p[q + 2]) / 3;
                    p[q] = h;
                    p[q + 1] = h;
                    p[q + 2] = h
                }
            }
            k.putImageData(g, 0, 0)
        },supports: function(h) {
            var i = fabric.document.createElement("canvas");
            if (typeof G_vmlCanvasManager !== "undefined") {
                G_vmlCanvasManager.initElement(i)
            }
            if (!i || !i.getContext) {
                return null
            }
            var g = i.getContext("2d");
            if (!g) {
                return null
            }
            switch (h) {
                case "getImageData":
                    return typeof g.getImageData !== "undefined";
                case "toDataURL":
                    return typeof i.toDataURL !== "undefined";
                default:
                    return null
            }
        }});
    fabric.StaticCanvas.prototype.toJSON = fabric.StaticCanvas.prototype.toObject
})(typeof exports != "undefined" ? exports : this);
(function() {
    var j = fabric.util.object.extend, q = fabric.util.getPointer, b = fabric.util.addListener, g = fabric.util.removeListener, m = {tr: "ne-resize",br: "se-resize",bl: "sw-resize",tl: "nw-resize",ml: "w-resize",mt: "n-resize",mr: "e-resize",mb: "s-resize"}, d = fabric.util.array.min, h = fabric.util.array.max, n = Math.sqrt, i = Math.pow, o = Math.atan2, p = Math.abs, f = Math.min, l = Math.max, a = 0.5;
    fabric.Canvas = function(s, r) {
        r || (r = {});
        this._initStatic(s, r);
        this._initInteractive();
        fabric.Canvas.activeInstance = this
    };
    function k() {
    }
    k.prototype = fabric.StaticCanvas.prototype;
    fabric.Canvas.prototype = new k;
    var e = {interactive: true,selection: true,selectionColor: "rgba(100, 100, 255, 0.3)",selectionBorderColor: "rgba(255, 255, 255, 0.3)",selectionLineWidth: 1,freeDrawingColor: "rgb(0, 0, 0)",freeDrawingLineWidth: 1,HOVER_CURSOR: "move",CURSOR: "default",CONTAINER_CLASS: "canvas-container",_initInteractive: function() {
            this._currentTransform = null;
            this._groupSelector = null;
            this._freeDrawingXPoints = [];
            this._freeDrawingYPoints = [];
            this._initWrapperElement();
            this._createUpperCanvas();
            this._initEvents();
            this.calcOffset()
        },_initEvents: function() {
            var r = this;
            this._onMouseDown = function(s) {
                r.__onMouseDown(s);
                b(fabric.document, "mouseup", r._onMouseUp);
                fabric.isTouchSupported && b(fabric.document, "touchend", r._onMouseUp);
                b(fabric.document, "mousemove", r._onMouseMove);
                fabric.isTouchSupported && b(fabric.document, "touchmove", r._onMouseMove);
                g(r.upperCanvasEl, "mousemove", r._onMouseMove);
                fabric.isTouchSupported && g(r.upperCanvasEl, "touchmove", r._onMouseMove)
            };
            this._onMouseUp = function(s) {
                r.__onMouseUp(s);
                g(fabric.document, "mouseup", r._onMouseUp);
                fabric.isTouchSupported && g(fabric.document, "touchend", r._onMouseUp);
                g(fabric.document, "mousemove", r._onMouseMove);
                fabric.isTouchSupported && g(fabric.document, "touchmove", r._onMouseMove);
                b(r.upperCanvasEl, "mousemove", r._onMouseMove);
                fabric.isTouchSupported && b(r.upperCanvasEl, "touchmove", r._onMouseMove)
            };
            this._onMouseMove = function(s) {
                s.preventDefault && s.preventDefault();
                r.__onMouseMove(s)
            };
            this._onResize = function(s) {
                r.calcOffset()
            };
            b(fabric.window, "resize", this._onResize);
            if (fabric.isTouchSupported) {
                b(this.upperCanvasEl, "touchstart", this._onMouseDown);
                b(this.upperCanvasEl, "touchmove", this._onMouseMove)
            } else {
                b(this.upperCanvasEl, "mousedown", this._onMouseDown);
                b(this.upperCanvasEl, "mousemove", this._onMouseMove)
            }
        },__onMouseUp: function(v) {
            if (this.isDrawingMode && this._isCurrentlyDrawing) {
                this._finalizeDrawingPath();
                return
            }
            if (this._currentTransform) {
                var r = this._currentTransform, u = r.target;
                if (u._scaling) {
                    u._scaling = false
                }
                var s = this._objects.length;
                while (s--) {
                    this._objects[s].setCoords()
                }
                if (this.stateful && u.hasStateChanged()) {
                    u.isMoving = false;
                    this.fire("object:modified", {target: u})
                }
            }
            this._currentTransform = null;
            if (this._groupSelector) {
                this._findSelectedObjects(v)
            }
            var t = this.getActiveGroup();
            if (t) {
                t.setObjectsCoords();
                t.set("isMoving", false);
                this._setCursor(this.CURSOR)
            }
            this._groupSelector = null;
            this.renderAll();
            this._setCursorFromEvent(v, u);
            this._setCursor("");
            var w = this;
            setTimeout(function() {
                w._setCursorFromEvent(v, u)
            }, 50);
            this.fire("mouse:up", {target: u,e: v})
        },__onMouseDown: function(w) {
            var r = "which" in w ? w.which == 1 : w.button == 1;
            if (!r && !fabric.isTouchSupported) {
                return
            }
            if (this.isDrawingMode) {
                this._prepareForDrawing(w);
                this._captureDrawingPath(w);
                return
            }
            if (this._currentTransform) {
                return
            }
            var v = this.findTarget(w), x = this.getPointer(w), t = this.getActiveGroup(), u;
            if (this._shouldClearSelection(w)) {
                this._groupSelector = {ex: x.x,ey: x.y,top: 0,left: 0};
                this.deactivateAllWithDispatch()
            } else {
                this.stateful && v.saveState();
                if (u = v._findTargetCorner(w, this._offset)) {
                    this.onBeforeScaleRotate(v)
                }
                this._setupCurrentTransform(w, v);
                var s = w.shiftKey && (t || this.getActiveObject());
                if (s) {
                    this._handleGroupLogic(w, v)
                } else {
                    if (v !== this.getActiveGroup()) {
                        this.deactivateAll()
                    }
                    this.setActiveObject(v, w)
                }
            }
            this.renderAll();
            this.fire("mouse:down", {target: v,e: w})
        },__onMouseMove: function(w) {
            if (this.isDrawingMode) {
                if (this._isCurrentlyDrawing) {
                    this._captureDrawingPath(w)
                }
                return
            }
            var s = this._groupSelector;
            if (s !== null) {
                var z = q(w);
                s.left = z.x - this._offset.left - s.ex;
                s.top = z.y - this._offset.top - s.ey;
                this.renderTop()
            } else {
                if (!this._currentTransform) {
                    var u = this.upperCanvasEl.style;
                    var v = this.findTarget(w);
                    if (!v) {
                        for (var t = this._objects.length; t--; ) {
                            if (this._objects[t] && !this._objects[t].active) {
                                this._objects[t].setActive(false)
                            }
                        }
                        u.cursor = this.CURSOR
                    } else {
                        this._setCursorFromEvent(w, v);
                        if (v.isActive()) {
                            v.setCornersVisibility && v.setCornersVisibility(true)
                        }
                    }
                } else {
                    var z = q(w), r = z.x, A = z.y;
                    this._currentTransform.target.isMoving = true;
                    if (this._currentTransform.action === "rotate") {
                        if (!w.shiftKey) {
                            this._rotateObject(r, A);
                            this.fire("object:rotating", {target: this._currentTransform.target})
                        }
                        this._scaleObject(r, A);
                        this.fire("object:scaling", {target: this._currentTransform.target})
                    } else {
                        if (this._currentTransform.action === "scaleX") {
                            this._scaleObject(r, A, "x");
                            this.fire("object:scaling", {target: this._currentTransform.target})
                        } else {
                            if (this._currentTransform.action === "scaleY") {
                                this._scaleObject(r, A, "y");
                                this.fire("object:scaling", {target: this._currentTransform.target})
                            } else {
                                this._translateObject(r, A);
                                this.fire("object:moving", {target: this._currentTransform.target})
                            }
                        }
                    }
                    this.renderAll()
                }
            }
            this.fire("mouse:move", {target: v,e: w})
        },containsPoint: function(w, v) {
            var z = this.getPointer(w), u = this._normalizePointer(v, z), r = u.x, A = u.y;
            var s = v._getImageLines(v.oCoords), t = v._findCrossPoints(r, A, s);
            if ((t && t % 2 === 1) || v._findTargetCorner(w, this._offset)) {
                return true
            }
            return false
        },_normalizePointer: function(s, v) {
            var u = this.getActiveGroup(), r = v.x, w = v.y;
            var t = (u && s.type !== "group" && u.contains(s));
            if (t) {
                r -= u.left;
                w -= u.top
            }
            return {x: r,y: w}
        },_shouldClearSelection: function(t) {
            var s = this.findTarget(t), r = this.getActiveGroup();
            return (!s || (s && r && !r.contains(s) && r !== s && !t.shiftKey))
        },_setupCurrentTransform: function(u, t) {
            var s = "drag", r, v = q(u);
            if (r = t._findTargetCorner(u, this._offset)) {
                s = (r === "ml" || r === "mr") ? "scaleX" : (r === "mt" || r === "mb") ? "scaleY" : "rotate"
            }
            this._currentTransform = {target: t,action: s,scaleX: t.scaleX,scaleY: t.scaleY,offsetX: v.x - t.left,offsetY: v.y - t.top,ex: v.x,ey: v.y,left: t.left,top: t.top,theta: t.theta,width: t.width * t.scaleX};
            this._currentTransform.original = {left: t.left,top: t.top}
        },_handleGroupLogic: function(u, t) {
            if (t.isType("group")) {
                t = this.findTarget(u, true);
                if (!t || t.isType("group")) {
                    return
                }
            }
            var r = this.getActiveGroup();
            if (r) {
                if (r.contains(t)) {
                    r.remove(t);
                    t.setActive(false);
                    if (r.size() === 1) {
                        this.discardActiveGroup()
                    }
                } else {
                    r.add(t)
                }
                this.fire("selection:created", {target: r,e: u});
                r.setActive(true)
            } else {
                if (this._activeObject) {
                    if (t !== this._activeObject) {
                        var s = new fabric.Group([this._activeObject, t]);
                        this.setActiveGroup(s);
                        r = this.getActiveGroup()
                    }
                }
                t.setActive(true)
            }
            if (r) {
                r.saveCoords()
            }
        },_prepareForDrawing: function(r) {
            this._isCurrentlyDrawing = true;
            this.discardActiveObject().renderAll();
            var s = this.getPointer(r);
            this._freeDrawingXPoints.length = this._freeDrawingYPoints.length = 0;
            this._freeDrawingXPoints.push(s.x);
            this._freeDrawingYPoints.push(s.y);
            this.contextTop.beginPath();
            this.contextTop.moveTo(s.x, s.y);
            this.contextTop.strokeStyle = this.freeDrawingColor;
            this.contextTop.lineWidth = this.freeDrawingLineWidth;
            this.contextTop.lineCap = this.contextTop.lineJoin = "round"
        },_captureDrawingPath: function(r) {
            var s = this.getPointer(r);
            this._freeDrawingXPoints.push(s.x);
            this._freeDrawingYPoints.push(s.y);
            this.contextTop.lineTo(s.x, s.y);
            this.contextTop.stroke()
        },_finalizeDrawingPath: function() {
            this.contextTop.closePath();
            this._isCurrentlyDrawing = false;
            var w = d(this._freeDrawingXPoints), v = d(this._freeDrawingYPoints), t = h(this._freeDrawingXPoints), r = h(this._freeDrawingYPoints), B = this.contextTop, C = [], x, z, s = this._freeDrawingXPoints, A = this._freeDrawingYPoints;
            C.push("M ", s[0] - w, " ", A[0] - v, " ");
            for (var y = 1; x = s[y], z = A[y]; y++) {
                C.push("L ", x - w, " ", z - v, " ")
            }
            C = C.join("");
            if (C === "M 0 0 L 0 0 ") {
                return
            }
            var u = new fabric.Path(C);
            u.fill = null;
            u.stroke = this.freeDrawingColor;
            u.strokeWidth = this.freeDrawingLineWidth;
            this.add(u);
            u.set("left", w + (t - w) / 2).set("top", v + (r - v) / 2).setCoords();
            this.renderAll();
            this.fire("path:created", {path: u})
        },_translateObject: function(r, t) {
            var s = this._currentTransform.target;
            s.lockMovementX || s.set("left", r - this._currentTransform.offsetX);
            s.lockMovementY || s.set("top", t - this._currentTransform.offsetY)
        },_scaleObject: function(r, B, z) {
            var s = this._currentTransform, A = this._offset, w = s.target;
            if (w.lockScalingX && w.lockScalingY) {
                return
            }
            var v = n(i(s.ey - s.top - A.top, 2) + i(s.ex - s.left - A.left, 2)), u = n(i(B - s.top - A.top, 2) + i(r - s.left - A.left, 2));
            w._scaling = true;
            if (!z) {
                w.lockScalingX || w.set("scaleX", s.scaleX * u / v);
                w.lockScalingY || w.set("scaleY", s.scaleY * u / v)
            } else {
                if (z === "x" && !w.lockUniScaling) {
                    w.lockScalingX || w.set("scaleX", s.scaleX * u / v)
                } else {
                    if (z === "y" && !w.lockUniScaling) {
                        w.lockScalingY || w.set("scaleY", s.scaleY * u / v)
                    }
                }
            }
        },_rotateObject: function(r, z) {
            var s = this._currentTransform, v = this._offset;
            if (s.target.lockRotation) {
                return
            }
            var u = o(s.ey - s.top - v.top, s.ex - s.left - v.left), w = o(z - s.top - v.top, r - s.left - v.left);
            s.target.set("theta", (w - u) + s.theta)
        },_setCursor: function(r) {
            this.upperCanvasEl.style.cursor = r
        },_setCursorFromEvent: function(w, v) {
            var t = this.upperCanvasEl.style;
            if (!v) {
                t.cursor = this.CURSOR;
                return false
            } else {
                var r = this.getActiveGroup();
                var u = !!v._findTargetCorner && (!r || !r.contains(v)) && v._findTargetCorner(w, this._offset);
                if (!u) {
                    t.cursor = this.HOVER_CURSOR
                } else {
                    if (u in m) {
                        t.cursor = m[u]
                    } else {
                        t.cursor = this.CURSOR;
                        return false
                    }
                }
            }
            return true
        },_drawSelection: function() {
            var r = this._groupSelector, v = r.left, u = r.top, t = p(v), s = p(u);
            this.contextTop.fillStyle = this.selectionColor;
            this.contextTop.fillRect(r.ex - ((v > 0) ? 0 : -v), r.ey - ((u > 0) ? 0 : -u), t, s);
            this.contextTop.lineWidth = this.selectionLineWidth;
            this.contextTop.strokeStyle = this.selectionBorderColor;
            this.contextTop.strokeRect(r.ex + a - ((v > 0) ? 0 : t), r.ey + a - ((u > 0) ? 0 : s), t, s)
        },_findSelectedObjects: function(x) {
            var y, t, D = [], s = this._groupSelector.ex, C = this._groupSelector.ey, r = s + this._groupSelector.left, A = C + this._groupSelector.top, z, w = new fabric.Point(f(s, r), f(C, A)), B = new fabric.Point(l(s, r), l(C, A));
            for (var u = 0, v = this._objects.length; u < v; ++u) {
                z = this._objects[u];
                if (!z) {
                    continue
                }
                if (z.intersectsWithRect(w, B) || z.isContainedWithinRect(w, B)) {
                    if (this.selection && z.selectable) {
                        z.setActive(true);
                        D.push(z)
                    }
                }
            }
            if (D.length === 1) {
                this.setActiveObject(D[0], x)
            } else {
                if (D.length > 1) {
                    var D = new fabric.Group(D);
                    this.setActiveGroup(D);
                    D.saveCoords();
                    this.fire("selection:created", {target: D})
                }
            }
            this.renderAll()
        },findTarget: function(v, s) {
            var u, w = this.getPointer(v);
            var t = this.getActiveGroup();
            if (t && !s && this.containsPoint(v, t)) {
                u = t;
                return u
            }
            for (var r = this._objects.length; r--; ) {
                if (this._objects[r] && this.containsPoint(v, this._objects[r])) {
                    u = this._objects[r];
                    this.relatedTarget = u;
                    break
                }
            }
            if (u && u.selectable) {
                return u
            }
        },getPointer: function(r) {
            var s = q(r);
            return {x: s.x - this._offset.left,y: s.y - this._offset.top}
        },_createUpperCanvas: function() {
            this.upperCanvasEl = this._createCanvasElement();
            this.upperCanvasEl.className = "upper-canvas";
            this.wrapperEl.appendChild(this.upperCanvasEl);
            this._applyCanvasStyle(this.upperCanvasEl);
            this.contextTop = this.upperCanvasEl.getContext("2d")
        },_initWrapperElement: function() {
            this.wrapperEl = fabric.util.wrapElement(this.lowerCanvasEl, "div", {"class": this.CONTAINER_CLASS});
            fabric.util.setStyle(this.wrapperEl, {width: this.getWidth() + "px",height: this.getHeight() + "px",position: "relative"});
            fabric.util.makeElementUnselectable(this.wrapperEl)
        },_applyCanvasStyle: function(s) {
            var t = this.getWidth() || s.width, r = this.getHeight() || s.height;
            fabric.util.setStyle(s, {position: "absolute",width: t + "px",height: r + "px",left: 0,top: 0});
            s.width = t;
            s.height = r;
            fabric.util.makeElementUnselectable(s)
        },getContext: function() {
            return this.contextTop
        },setActiveObject: function(r, s) {
            if (this._activeObject) {
                this._activeObject.setActive(false)
            }
            this._activeObject = r;
            r.setActive(true);
            this.renderAll();
            this.fire("object:selected", {target: r,e: s});
            return this
        },getActiveObject: function() {
            return this._activeObject
        },discardActiveObject: function() {
            if (this._activeObject) {
                this._activeObject.setActive(false)
            }
            this._activeObject = null;
            return this
        },setActiveGroup: function(r) {
            this._activeGroup = r;
            return this
        },getActiveGroup: function() {
            return this._activeGroup
        },discardActiveGroup: function() {
            var r = this.getActiveGroup();
            if (r) {
                r.destroy()
            }
            return this.setActiveGroup(null)
        },deactivateAll: function() {
            var s = this.getObjects(), t = 0, r = s.length;
            for (; t < r; t++) {
                s[t].setActive(false)
            }
            this.discardActiveGroup();
            this.discardActiveObject();
            return this
        },deactivateAllWithDispatch: function() {
            var r = this.getActiveGroup() || this.getActiveObject();
            if (r) {
                this.fire("before:selection:cleared", {target: r})
            }
            this.deactivateAll();
            if (r) {
                this.fire("selection:cleared")
            }
            return this
        }};
    fabric.Canvas.prototype.toString = fabric.StaticCanvas.prototype.toString;
    j(fabric.Canvas.prototype, e);
    for (var c in fabric.StaticCanvas) {
        if (c !== "prototype") {
            fabric.Canvas[c] = fabric.StaticCanvas[c]
        }
    }
    if (fabric.isTouchSupported) {
        fabric.Canvas.prototype._setCursorFromEvent = function() {
        }
    }
    fabric.Element = fabric.Canvas
})();
fabric.util.object.extend(fabric.StaticCanvas.prototype, {fxCenterObjectH: function(b, c) {
        c = c || {};
        var d = function() {
        }, e = c.onComplete || d, a = c.onChange || d, f = this;
        fabric.util.animate({startValue: b.get("left"),endValue: this.getCenter().left,duration: this.FX_DURATION,onChange: function(g) {
                b.set("left", g);
                f.renderAll();
                a()
            },onComplete: function() {
                b.setCoords();
                e()
            }});
        return this
    },fxCenterObjectV: function(b, c) {
        c = c || {};
        var d = function() {
        }, e = c.onComplete || d, a = c.onChange || d, f = this;
        fabric.util.animate({startValue: b.get("top"),endValue: this.getCenter().top,duration: this.FX_DURATION,onChange: function(g) {
                b.set("top", g);
                f.renderAll();
                a()
            },onComplete: function() {
                b.setCoords();
                e()
            }});
        return this
    },fxStraightenObject: function(a) {
        a.fxStraighten({onChange: this.renderAll.bind(this)});
        return this
    },fxRemove: function(a, c) {
        var b = this;
        a.fxRemove({onChange: this.renderAll.bind(this),onComplete: function() {
                b.remove(a);
                if (typeof c === "function") {
                    c()
                }
            }});
        return this
    }});
fabric.util.object.extend(fabric.StaticCanvas.prototype, {loadFromDatalessJSON: function(a, c) {
        if (!a) {
            return
        }
        var b = (typeof a === "string") ? JSON.parse(a) : a;
        if (!b || (b && !b.objects)) {
            return
        }
        this.clear();
        this.backgroundColor = b.background;
        this._enlivenDatalessObjects(b.objects, c)
    },_enlivenDatalessObjects: function(b, h) {
        function g(i, e) {
            f.insertAt(i, e, true);
            i.setCoords();
            if (++d === a) {
                h && h()
            }
        }
        var f = this, d = 0, a = b.length;
        if (a === 0 && h) {
            h()
        }
        try {
            b.forEach(function(n, l) {
                var i = n.paths ? "paths" : "path";
                var m = n[i];
                delete n[i];
                if (typeof m !== "string") {
                    switch (n.type) {
                        case "image":
                            fabric[fabric.util.string.capitalize(n.type)].fromObject(n, function(p) {
                                g(p, l)
                            });
                            break;
                        default:
                            var e = fabric[fabric.util.string.camelize(fabric.util.string.capitalize(n.type))];
                            if (e && e.fromObject) {
                                if (m) {
                                    n[i] = m
                                }
                                g(e.fromObject(n), l)
                            }
                            break
                    }
                } else {
                    if (n.type === "image") {
                        fabric.util.loadImage(m, function(p) {
                            var o = new fabric.Image(p);
                            o.setSourcePath(m);
                            fabric.util.object.extend(o, n);
                            o.setAngle(n.angle);
                            g(o, l)
                        })
                    } else {
                        if (n.type === "text") {
                            n.path = m;
                            var k = fabric.Text.fromObject(n);
                            var j = function() {
                                if (Object.prototype.toString.call(fabric.window.opera) === "[object Opera]") {
                                    setTimeout(function() {
                                        g(k, l)
                                    }, 500)
                                } else {
                                    g(k, l)
                                }
                            };
                            fabric.util.getScript(m, j)
                        } else {
                            fabric.loadSVGFromURL(m, function(q, p) {
                                if (q.length > 1) {
                                    var o = new fabric.PathGroup(q, n)
                                } else {
                                    var o = q[0]
                                }
                                o.setSourcePath(m);
                                if (!(o instanceof fabric.PathGroup)) {
                                    fabric.util.object.extend(o, n);
                                    if (typeof n.angle !== "undefined") {
                                        o.setAngle(n.angle)
                                    }
                                }
                                g(o, l)
                            })
                        }
                    }
                }
            }, this)
        } catch (c) {
            fabric.log(c.message)
        }
    },loadFromJSON: function(a, d) {
        if (!a) {
            return
        }
        var b = JSON.parse(a);
        if (!b || (b && !b.objects)) {
            return
        }
        this.clear();
        var c = this;
        this._enlivenObjects(b.objects, function() {
            c.backgroundColor = b.background;
            if (d) {
                d()
            }
        });
        return this
    },_enlivenObjects: function(b, e) {
        var c = 0, a = b.filter(function(f) {
            return f.type === "image"
        }).length;
        var d = this;
        b.forEach(function(h, g) {
            if (!h.type) {
                return
            }
            switch (h.type) {
                case "image":
                case "font":
                    fabric[fabric.util.string.capitalize(h.type)].fromObject(h, function(i) {
                        d.insertAt(i, g, true);
                        if (++c === a) {
                            if (e) {
                                e()
                            }
                        }
                    });
                    break;
                default:
                    var f = fabric[fabric.util.string.camelize(fabric.util.string.capitalize(h.type))];
                    if (f && f.fromObject) {
                        d.insertAt(f.fromObject(h), g, true)
                    }
                    break
            }
        });
        if (a === 0 && e) {
            e()
        }
    },_toDataURL: function(a, b) {
        this.clone(function(c) {
            b(c.toDataURL(a))
        })
    },_toDataURLWithMultiplier: function(a, c, b) {
        this.clone(function(d) {
            b(d.toDataURLWithMultiplier(a, c))
        })
    },clone: function(b) {
        var a = JSON.stringify(this);
        this.cloneWithoutData(function(c) {
            c.loadFromJSON(a, function() {
                if (b) {
                    b(c)
                }
            })
        })
    },cloneWithoutData: function(c) {
        var a = fabric.document.createElement("canvas");
        a.width = this.getWidth();
        a.height = this.getHeight();
        var b = this.__clone || (this.__clone = new fabric.Canvas(a));
        b.clipTo = this.clipTo;
        if (c) {
            c(b)
        }
    }});
(function(a) {
    var d = a.fabric || (a.fabric = {}), l = d.util.object.extend, m = d.util.object.clone, c = d.util.toFixed, k = d.util.string.capitalize, p = d.util.getPointer, b = d.util.degreesToRadians, o = Array.prototype.slice;
    if (d.Object) {
        return
    }
    d.Object = d.util.createClass({type: "object",includeDefaultValues: true,NUM_FRACTION_DIGITS: 2,FX_DURATION: 500,MIN_SCALE_LIMIT: 0.1,stateProperties: ("top left width height scaleX scaleY flipX flipY theta angle opacity cornersize fill overlayFill stroke strokeWidth fillRule borderScaleFactor transformMatrix selectable").split(" "),top: 0,left: 0,width: 0,height: 0,scaleX: 1,scaleY: 1,flipX: false,flipY: false,theta: 0,opacity: 1,angle: 0,cornersize: 12,padding: 0,borderColor: "rgba(102,153,255,0.75)",cornerColor: "rgba(102,153,255,0.5)",fill: "rgb(0,0,0)",fillRule: "source-over",overlayFill: null,stroke: null,strokeWidth: 1,borderOpacityWhenMoving: 0.4,borderScaleFactor: 1,transformMatrix: null,selectable: true,hasControls: true,hasBorders: true,callSuper: function(i) {
            var q = this.constructor.superclass.prototype[i];
            return (arguments.length > 1) ? q.apply(this, o.call(arguments, 1)) : q.call(this)
        },initialize: function(i) {
            i && this.setOptions(i)
        },setOptions: function(q) {
            var r = this.stateProperties.length, s;
            while (r--) {
                s = this.stateProperties[r];
                if (s in q) {
                    this.set(s, q[s])
                }
            }
        },transform: function(i) {
            i.globalAlpha = this.opacity;
            i.translate(this.left, this.top);
            i.rotate(this.theta);
            i.scale(this.scaleX * (this.flipX ? -1 : 1), this.scaleY * (this.flipY ? -1 : 1))
        },toObject: function() {
            var i = {type: this.type,left: c(this.left, this.NUM_FRACTION_DIGITS),top: c(this.top, this.NUM_FRACTION_DIGITS),width: c(this.width, this.NUM_FRACTION_DIGITS),height: c(this.height, this.NUM_FRACTION_DIGITS),fill: this.fill,overlayFill: this.overlayFill,stroke: this.stroke,strokeWidth: this.strokeWidth,scaleX: c(this.scaleX, this.NUM_FRACTION_DIGITS),scaleY: c(this.scaleY, this.NUM_FRACTION_DIGITS),angle: c(this.getAngle(), this.NUM_FRACTION_DIGITS),flipX: this.flipX,flipY: this.flipY,opacity: c(this.opacity, this.NUM_FRACTION_DIGITS),selectable: this.selectable};
            if (!this.includeDefaultValues) {
                i = this._removeDefaultValues(i)
            }
            return i
        },toDatalessObject: function() {
            return this.toObject()
        },getSvgStyles: function() {
            return ["stroke: ", (this.stroke ? this.stroke : "none"), "; ", "stroke-width: ", (this.strokeWidth ? this.strokeWidth : "0"), "; ", "fill: ", (this.fill ? this.fill : "none"), "; ", "opacity: ", (this.opacity ? this.opacity : "1"), ";"].join("")
        },getSvgTransform: function() {
            var i = this.getAngle();
            return ["translate(", c(this.left, 2), " ", c(this.top, 2), ")", i !== 0 ? (" rotate(" + c(i, 2) + ")") : "", (this.scaleX === 1 && this.scaleY === 1) ? "" : (" scale(" + c(this.scaleX, 2) + " " + c(this.scaleY, 2) + ")")].join("")
        },_removeDefaultValues: function(q) {
            var i = d.Object.prototype.options;
            if (i) {
                this.stateProperties.forEach(function(r) {
                    if (q[r] === i[r]) {
                        delete q[r]
                    }
                })
            }
            return q
        },isActive: function() {
            return !!this.active
        },setActive: function(i) {
            this.active = !!i;
            return this
        },toString: function() {
            return "#<fabric." + k(this.type) + ">"
        },set: function(q, i) {
            var s = (q === "scaleX" || q === "scaleY") && i < this.MIN_SCALE_LIMIT;
            if (s) {
                i = this.MIN_SCALE_LIMIT
            }
            if (typeof q == "object") {
                for (var r in q) {
                    this.set(r, q[r])
                }
            } else {
                if (q === "angle") {
                    this.setAngle(i)
                } else {
                    this[q] = i
                }
            }
            return this
        },toggle: function(q) {
            var i = this.get(q);
            if (typeof i === "boolean") {
                this.set(q, !i)
            }
            return this
        },setSourcePath: function(i) {
            this.sourcePath = i;
            return this
        },get: function(i) {
            return (i === "angle") ? this.getAngle() : this[i]
        },render: function(q, r) {
            if (this.width === 0 || this.height === 0) {
                return
            }
            q.save();
            var i = this.transformMatrix;
            if (i) {
                q.setTransform(i[0], i[1], i[2], i[3], i[4], i[5])
            }
            if (!r) {
                this.transform(q)
            }
            if (this.stroke) {
                q.lineWidth = this.strokeWidth;
                q.strokeStyle = this.stroke
            }
            if (this.overlayFill) {
                q.fillStyle = this.overlayFill
            } else {
                if (this.fill) {
                    q.fillStyle = this.fill
                }
            }
            if (this.group) {
            }
            this._render(q, r);
            if (this.active && !r) {
                this.drawBorders(q);
                this.hideCorners || this.drawCorners(q)
            }
            q.restore()
        },getWidth: function() {
            return this.width * this.scaleX
        },getHeight: function() {
            return this.height * this.scaleY
        },scale: function(i) {
            this.scaleX = i;
            this.scaleY = i;
            return this
        },scaleToWidth: function(i) {
            return this.scale(i / this.width)
        },scaleToHeight: function(i) {
            return this.scale(i / this.height)
        },setOpacity: function(i) {
            this.set("opacity", i);
            return this
        },getAngle: function() {
            return this.theta * 180 / Math.PI
        },setAngle: function(i) {
            this.theta = i / 180 * Math.PI;
            this.angle = i;
            return this
        },setCoords: function() {
            this.currentWidth = this.width * this.scaleX;
            this.currentHeight = this.height * this.scaleY;
            this._hypotenuse = Math.sqrt(Math.pow(this.currentWidth / 2, 2) + Math.pow(this.currentHeight / 2, 2));
            this._angle = Math.atan(this.currentHeight / this.currentWidth);
            var v = Math.cos(this._angle + this.theta) * this._hypotenuse, u = Math.sin(this._angle + this.theta) * this._hypotenuse, r = this.theta, y = Math.sin(r), w = Math.cos(r);
            var B = {x: this.left - v,y: this.top - u};
            var x = {x: B.x + (this.currentWidth * w),y: B.y + (this.currentWidth * y)};
            var A = {x: x.x - (this.currentHeight * y),y: x.y + (this.currentHeight * w)};
            var s = {x: B.x - (this.currentHeight * y),y: B.y + (this.currentHeight * w)};
            var t = {x: B.x - (this.currentHeight / 2 * y),y: B.y + (this.currentHeight / 2 * w)};
            var i = {x: B.x + (this.currentWidth / 2 * w),y: B.y + (this.currentWidth / 2 * y)};
            var q = {x: x.x - (this.currentHeight / 2 * y),y: x.y + (this.currentHeight / 2 * w)};
            var z = {x: s.x + (this.currentWidth / 2 * w),y: s.y + (this.currentWidth / 2 * y)};
            this.oCoords = {tl: B,tr: x,br: A,bl: s,ml: t,mt: i,mr: q,mb: z};
            this._setCornerCoords();
            return this
        },drawBorders: function(q) {
            if (!this.hasBorders) {
                return
            }
            var v = this.padding, s = v * 2;
            q.save();
            q.globalAlpha = this.isMoving ? this.borderOpacityWhenMoving : 1;
            q.strokeStyle = this.borderColor;
            var t = 1 / (this.scaleX < this.MIN_SCALE_LIMIT ? this.MIN_SCALE_LIMIT : this.scaleX), r = 1 / (this.scaleY < this.MIN_SCALE_LIMIT ? this.MIN_SCALE_LIMIT : this.scaleY);
            q.lineWidth = 1 / this.borderScaleFactor;
            q.scale(t, r);
            var i = this.getWidth(), u = this.getHeight();
            q.strokeRect(~~(-(i / 2) - v) + 0.5, ~~(-(u / 2) - v) + 0.5, ~~(i + s), ~~(u + s));
            q.restore();
            return this
        },drawCorners: function(B) {
            if (!this.hasControls) {
                return
            }
            var D = this.cornersize, y = D / 2, x = this.padding, r = -(this.width / 2), w = -(this.height / 2), C, A, q = D / this.scaleX, i = D / this.scaleY, s = (x + y) / this.scaleY, t = (x + y) / this.scaleX, v = (x + y - D) / this.scaleX, u = (x + y - D) / this.scaleY, z = this.height;
            B.save();
            B.globalAlpha = this.isMoving ? this.borderOpacityWhenMoving : 1;
            B.fillStyle = this.cornerColor;
            C = r - t;
            A = w - s;
            B.fillRect(C, A, q, i);
            C = r + this.width - t;
            A = w - s;
            B.fillRect(C, A, q, i);
            C = r - t;
            A = w + z + u;
            B.fillRect(C, A, q, i);
            C = r + this.width + v;
            A = w + z + u;
            B.fillRect(C, A, q, i);
            C = r + this.width / 2 - t;
            A = w - s;
            B.fillRect(C, A, q, i);
            C = r + this.width / 2 - t;
            A = w + z + u;
            B.fillRect(C, A, q, i);
            C = r + this.width + v;
            A = w + z / 2 - s;
            B.fillRect(C, A, q, i);
            C = r - t;
            A = w + z / 2 - s;
            B.fillRect(C, A, q, i);
            B.restore();
            return this
        },clone: function(i) {
            if (this.constructor.fromObject) {
                return this.constructor.fromObject(this.toObject(), i)
            }
            return new d.Object(this.toObject())
        },cloneAsImage: function(s) {
            if (d.Image) {
                var q = new Image();
                q.onload = function() {
                    if (s) {
                        s(new d.Image(q), r)
                    }
                    q = q.onload = null
                };
                var r = {angle: this.get("angle"),flipX: this.get("flipX"),flipY: this.get("flipY")};
                this.set("angle", 0).set("flipX", false).set("flipY", false);
                this.toDataURL(function(i) {
                    q.src = i
                })
            }
            return this
        },toDataURL: function(s) {
            var q = d.document.createElement("canvas");
            if (!q.getContext && typeof G_vmlCanvasManager != "undefined") {
                G_vmlCanvasManager.initElement(q)
            }
            q.width = this.getWidth();
            q.height = this.getHeight();
            d.util.wrapElement(q, "div");
            var i = new d.Canvas(q);
            i.backgroundColor = "transparent";
            i.renderAll();
            if (this.constructor.async) {
                this.clone(r)
            } else {
                r(this.clone())
            }
            function r(u) {
                u.left = q.width / 2;
                u.top = q.height / 2;
                u.setActive(false);
                i.add(u);
                var t = i.toDataURL("png");
                i.dispose();
                i = u = null;
                s && s(t)
            }
        },hasStateChanged: function() {
            return this.stateProperties.some(function(i) {
                return this[i] !== this.originalState[i]
            }, this)
        },saveState: function() {
            this.stateProperties.forEach(function(i) {
                this.originalState[i] = this.get(i)
            }, this);
            return this
        },setupState: function() {
            this.originalState = {};
            this.saveState()
        },intersectsWithRect: function(r, t) {
            var w = this.oCoords, i = new d.Point(w.tl.x, w.tl.y), s = new d.Point(w.tr.x, w.tr.y), v = new d.Point(w.bl.x, w.bl.y), q = new d.Point(w.br.x, w.br.y);
            var u = d.Intersection.intersectPolygonRectangle([i, s, q, v], r, t);
            return (u.status === "Intersection")
        },intersectsWithObject: function(i) {
            function q(u) {
                return {tl: new d.Point(u.tl.x, u.tl.y),tr: new d.Point(u.tr.x, u.tr.y),bl: new d.Point(u.bl.x, u.bl.y),br: new d.Point(u.br.x, u.br.y)}
            }
            var r = q(this.oCoords), t = q(i.oCoords);
            var s = d.Intersection.intersectPolygonPolygon([r.tl, r.tr, r.br, r.bl], [t.tl, t.tr, t.br, t.bl]);
            return (s.status === "Intersection")
        },isContainedWithinObject: function(i) {
            return this.isContainedWithinRect(i.oCoords.tl, i.oCoords.br)
        },isContainedWithinRect: function(r, t) {
            var v = this.oCoords, i = new d.Point(v.tl.x, v.tl.y), s = new d.Point(v.tr.x, v.tr.y), u = new d.Point(v.bl.x, v.bl.y), q = new d.Point(v.br.x, v.br.y);
            return i.x > r.x && s.x < t.x && i.y > r.y && u.y < t.y
        },isType: function(i) {
            return this.type === i
        },_findTargetCorner: function(v, x) {
            if (!this.hasControls) {
                return false
            }
            var w = p(v), u = w.x - x.left, r = w.y - x.top, t, q;
            for (var s in this.oCoords) {
                q = this._getImageLines(this.oCoords[s].corner, s);
                t = this._findCrossPoints(u, r, q);
                if (t % 2 == 1 && t != 0) {
                    this.__corner = s;
                    return s
                }
            }
            return false
        },_findCrossPoints: function(w, v, i) {
            var z, y, r, q, x, u, t = 0, s;
            for (var A in i) {
                s = i[A];
                if ((s.o.y < v) && (s.d.y < v)) {
                    continue
                }
                if ((s.o.y >= v) && (s.d.y >= v)) {
                    continue
                }
                if ((s.o.x == s.d.x) && (s.o.x >= w)) {
                    x = s.o.x;
                    u = v
                } else {
                    z = 0;
                    y = (s.d.y - s.o.y) / (s.d.x - s.o.x);
                    r = v - z * w;
                    q = s.o.y - y * s.o.x;
                    x = -(r - q) / (z - y);
                    u = r + z * x
                }
                if (x >= w) {
                    t += 1
                }
                if (t == 2) {
                    break
                }
            }
            return t
        },_getImageLines: function(r, q) {
            return {topline: {o: r.tl,d: r.tr},rightline: {o: r.tr,d: r.br},bottomline: {o: r.br,d: r.bl},leftline: {o: r.bl,d: r.tl}}
        },_setCornerCoords: function() {
            var t = this.oCoords, q = b(45 - this.getAngle()), s = Math.sqrt(2 * Math.pow(this.cornersize, 2)) / 2, i = s * Math.cos(q), r = s * Math.sin(q);
            t.tl.corner = {tl: {x: t.tl.x - r,y: t.tl.y - i},tr: {x: t.tl.x + i,y: t.tl.y - r},bl: {x: t.tl.x - i,y: t.tl.y + r},br: {x: t.tl.x + r,y: t.tl.y + i}};
            t.tr.corner = {tl: {x: t.tr.x - r,y: t.tr.y - i},tr: {x: t.tr.x + i,y: t.tr.y - r},br: {x: t.tr.x + r,y: t.tr.y + i},bl: {x: t.tr.x - i,y: t.tr.y + r}};
            t.bl.corner = {tl: {x: t.bl.x - r,y: t.bl.y - i},bl: {x: t.bl.x - i,y: t.bl.y + r},br: {x: t.bl.x + r,y: t.bl.y + i},tr: {x: t.bl.x + i,y: t.bl.y - r}};
            t.br.corner = {tr: {x: t.br.x + i,y: t.br.y - r},bl: {x: t.br.x - i,y: t.br.y + r},br: {x: t.br.x + r,y: t.br.y + i},tl: {x: t.br.x - r,y: t.br.y - i}};
            t.ml.corner = {tl: {x: t.ml.x - r,y: t.ml.y - i},tr: {x: t.ml.x + i,y: t.ml.y - r},bl: {x: t.ml.x - i,y: t.ml.y + r},br: {x: t.ml.x + r,y: t.ml.y + i}};
            t.mt.corner = {tl: {x: t.mt.x - r,y: t.mt.y - i},tr: {x: t.mt.x + i,y: t.mt.y - r},bl: {x: t.mt.x - i,y: t.mt.y + r},br: {x: t.mt.x + r,y: t.mt.y + i}};
            t.mr.corner = {tl: {x: t.mr.x - r,y: t.mr.y - i},tr: {x: t.mr.x + i,y: t.mr.y - r},bl: {x: t.mr.x - i,y: t.mr.y + r},br: {x: t.mr.x + r,y: t.mr.y + i}};
            t.mb.corner = {tl: {x: t.mb.x - r,y: t.mb.y - i},tr: {x: t.mb.x + i,y: t.mb.y - r},bl: {x: t.mb.x - i,y: t.mb.y + r},br: {x: t.mb.x + r,y: t.mb.y + i}}
        },toGrayscale: function() {
            var i = this.get("fill");
            if (i) {
                this.set("overlayFill", new d.Color(i).toGrayscale().toRgb())
            }
            return this
        },complexity: function() {
            return 0
        },straighten: function() {
            var i = this._getAngleValueForStraighten();
            this.setAngle(i);
            return this
        },fxStraighten: function(q) {
            q = q || {};
            var r = function() {
            }, s = q.onComplete || r, i = q.onChange || r, t = this;
            d.util.animate({startValue: this.get("angle"),endValue: this._getAngleValueForStraighten(),duration: this.FX_DURATION,onChange: function(u) {
                    t.setAngle(u);
                    i()
                },onComplete: function() {
                    t.setCoords();
                    s()
                },onStart: function() {
                    t.setActive(false)
                }});
            return this
        },fxRemove: function(q) {
            q || (q = {});
            var r = function() {
            }, s = q.onComplete || r, i = q.onChange || r, t = this;
            d.util.animate({startValue: this.get("opacity"),endValue: 0,duration: this.FX_DURATION,onChange: function(u) {
                    t.set("opacity", u);
                    i()
                },onComplete: s,onStart: function() {
                    t.setActive(false)
                }});
            return this
        },_getAngleValueForStraighten: function() {
            var i = this.get("angle");
            if (i > -225 && i <= -135) {
                return -180
            } else {
                if (i > -135 && i <= -45) {
                    return -90
                } else {
                    if (i > -45 && i <= 45) {
                        return 0
                    } else {
                        if (i > 45 && i <= 135) {
                            return 90
                        } else {
                            if (i > 135 && i <= 225) {
                                return 180
                            } else {
                                if (i > 225 && i <= 315) {
                                    return 270
                                } else {
                                    if (i > 315) {
                                        return 360
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return 0
        },toJSON: function() {
            return this.toObject()
        },setGradientFill: function(i, q) {
            this.set("fill", d.Gradient.forObject(this, i, q))
        },animate: function(q, s, i) {
            var r = this;
            if (!("from" in i)) {
                i.from = this.get(q)
            }
            if (/[+-]/.test(s.charAt(0))) {
                s = this.get(q) + parseFloat(s)
            }
            d.util.animate({startValue: i.from,endValue: s,duration: i.duration,onChange: function(t) {
                    r.set(q, t);
                    i.onChange && i.onChange()
                },onComplete: function() {
                    r.setCoords();
                    i.onComplete && i.onComplete()
                }})
        }});
    d.Object.prototype.rotate = d.Object.prototype.setAngle;
    var j = d.Object.prototype;
    for (var g = j.stateProperties.length; g--; ) {
        var h = j.stateProperties[g], e = h.charAt(0).toUpperCase() + h.slice(1), f = "set" + e, n = "get" + e;
        if (!j[n]) {
            j[n] = (function(i) {
                return new Function('return this.get("' + i + '")')
            })(h)
        }
        if (!j[f]) {
            j[f] = (function(i) {
                return new Function("value", 'return this.set("' + i + '", value)')
            })(h)
        }
    }
})(typeof exports != "undefined" ? exports : this);
(function(b) {
    var c = b.fabric || (b.fabric = {}), e = c.util.object.extend, d = c.Object.prototype.set, a = {x1: 1,x2: 1,y1: 1,y2: 1};
    if (c.Line) {
        c.warn("fabric.Line is already defined");
        return
    }
    c.Line = c.util.createClass(c.Object, {type: "line",initialize: function(g, f) {
            if (!g) {
                g = [0, 0, 0, 0]
            }
            this.callSuper("initialize", f);
            this.set("x1", g[0]);
            this.set("y1", g[1]);
            this.set("x2", g[2]);
            this.set("y2", g[3]);
            this._setWidthHeight()
        },_setWidthHeight: function() {
            this.set("width", (this.x2 - this.x1) || 1);
            this.set("height", (this.y2 - this.y1) || 1);
            this.set("left", this.x1 + this.width / 2);
            this.set("top", this.y1 + this.height / 2)
        },set: function(f, g) {
            d.call(this, f, g);
            if (f in a) {
                this._setWidthHeight()
            }
            return this
        },_render: function(f) {
            f.beginPath();
            f.moveTo(this.width === 1 ? 0 : (-this.width / 2), this.height === 1 ? 0 : (-this.height / 2));
            f.lineTo(this.width === 1 ? 0 : (this.width / 2), this.height === 1 ? 0 : (this.height / 2));
            f.lineWidth = this.strokeWidth;
            var g = f.strokeStyle;
            f.strokeStyle = f.fillStyle;
            f.stroke();
            f.strokeStyle = g
        },complexity: function() {
            return 1
        },toObject: function() {
            return e(this.callSuper("toObject"), {x1: this.get("x1"),y1: this.get("y1"),x2: this.get("x2"),y2: this.get("y2")})
        },toSVG: function() {
            return ["<line ", 'x1="', this.get("x1"), '" ', 'y1="', this.get("y1"), '" ', 'x2="', this.get("x2"), '" ', 'y2="', this.get("y2"), '" ', 'style="', this.getSvgStyles(), '" ', "/>"].join("")
        }});
    c.Line.ATTRIBUTE_NAMES = "x1 y1 x2 y2 stroke stroke-width transform".split(" ");
    c.Line.fromElement = function(g, f) {
        var i = c.parseAttributes(g, c.Line.ATTRIBUTE_NAMES);
        var h = [i.x1 || 0, i.y1 || 0, i.x2 || 0, i.y2 || 0];
        return new c.Line(h, e(i, f))
    };
    c.Line.fromObject = function(f) {
        var g = [f.x1, f.y1, f.x2, f.y2];
        return new c.Line(g, f)
    }
})(typeof exports != "undefined" ? exports : this);
(function(a) {
    var b = a.fabric || (a.fabric = {}), d = Math.PI * 2, e = b.util.object.extend;
    if (b.Circle) {
        b.warn("fabric.Circle is already defined.");
        return
    }
    b.Circle = b.util.createClass(b.Object, {type: "circle",initialize: function(f) {
            f = f || {};
            this.set("radius", f.radius || 0);
            this.callSuper("initialize", f);
            var g = this.get("radius") * 2 * this.get("scaleX");
            this.set("width", g).set("height", g)
        },toObject: function() {
            return e(this.callSuper("toObject"), {radius: this.get("radius")})
        },toSVG: function() {
            return ('<circle cx="0" cy="0" r="' + this.radius + '" style="' + this.getSvgStyles() + '" transform="' + this.getSvgTransform() + '" />')
        },_render: function(f, g) {
            f.beginPath();
            f.globalAlpha *= this.opacity;
            f.arc(g ? this.left : 0, g ? this.top : 0, this.radius, 0, d, false);
            f.closePath();
            if (this.fill) {
                f.fill()
            }
            if (this.stroke) {
                f.stroke()
            }
        },getRadiusX: function() {
            return this.get("radius") * this.get("scaleX")
        },getRadiusY: function() {
            return this.get("radius") * this.get("scaleY")
        },complexity: function() {
            return 1
        }});
    b.Circle.ATTRIBUTE_NAMES = "cx cy r fill fill-opacity opacity stroke stroke-width transform".split(" ");
    b.Circle.fromElement = function(g, f) {
        f || (f = {});
        var h = b.parseAttributes(g, b.Circle.ATTRIBUTE_NAMES);
        if (!c(h)) {
            throw Error("value of `r` attribute is required and can not be negative")
        }
        if ("left" in h) {
            h.left -= (f.width / 2) || 0
        }
        if ("top" in h) {
            h.top -= (f.height / 2) || 0
        }
        return new b.Circle(e(h, f))
    };
    function c(f) {
        return (("radius" in f) && (f.radius > 0))
    }
    b.Circle.fromObject = function(f) {
        return new b.Circle(f)
    }
})(typeof exports != "undefined" ? exports : this);
(function(a) {
    var b = a.fabric || (a.fabric = {});
    if (b.Triangle) {
        b.warn("fabric.Triangle is already defined");
        return
    }
    b.Triangle = b.util.createClass(b.Object, {type: "triangle",initialize: function(c) {
            c = c || {};
            this.callSuper("initialize", c);
            this.set("width", c.width || 100).set("height", c.height || 100)
        },_render: function(c) {
            var d = this.width / 2, e = this.height / 2;
            c.beginPath();
            c.moveTo(-d, e);
            c.lineTo(0, -e);
            c.lineTo(d, e);
            c.closePath();
            if (this.fill) {
                c.fill()
            }
            if (this.stroke) {
                c.stroke()
            }
        },complexity: function() {
            return 1
        },toSVG: function() {
            var c = this.width / 2, d = this.height / 2;
            var e = [-c + " " + d, "0 " + -d, c + " " + d].join(",");
            return '<polygon points="' + e + '" style="' + this.getSvgStyles() + '" transform="' + this.getSvgTransform() + '" />'
        }});
    b.Triangle.fromObject = function(c) {
        return new b.Triangle(c)
    }
})(typeof exports != "undefined" ? exports : this);
(function(a) {
    var b = a.fabric || (a.fabric = {}), c = Math.PI * 2, d = b.util.object.extend;
    if (b.Ellipse) {
        b.warn("fabric.Ellipse is already defined.");
        return
    }
    b.Ellipse = b.util.createClass(b.Object, {type: "ellipse",initialize: function(e) {
            e = e || {};
            this.callSuper("initialize", e);
            this.set("rx", e.rx || 0);
            this.set("ry", e.ry || 0);
            this.set("width", this.get("rx") * 2);
            this.set("height", this.get("ry") * 2)
        },toObject: function() {
            return d(this.callSuper("toObject"), {rx: this.get("rx"),ry: this.get("ry")})
        },toSVG: function() {
            return ["<ellipse ", 'rx="', this.get("rx"), '" ', 'ry="', this.get("ry"), '" ', 'style="', this.getSvgStyles(), '" ', 'transform="', this.getSvgTransform(), '" ', "/>"].join("")
        },render: function(e, f) {
            if (this.rx === 0 || this.ry === 0) {
                return
            }
            return this.callSuper("render", e, f)
        },_render: function(e, f) {
            e.beginPath();
            e.save();
            e.globalAlpha *= this.opacity;
            e.transform(1, 0, 0, this.ry / this.rx, 0, 0);
            e.arc(f ? this.left : 0, f ? this.top : 0, this.rx, 0, c, false);
            if (this.stroke) {
                e.stroke()
            }
            if (this.fill) {
                e.fill()
            }
            e.restore()
        },complexity: function() {
            return 1
        }});
    b.Ellipse.ATTRIBUTE_NAMES = "cx cy rx ry fill fill-opacity opacity stroke stroke-width transform".split(" ");
    b.Ellipse.fromElement = function(f, e) {
        e || (e = {});
        var g = b.parseAttributes(f, b.Ellipse.ATTRIBUTE_NAMES);
        if ("left" in g) {
            g.left -= (e.width / 2) || 0
        }
        if ("top" in g) {
            g.top -= (e.height / 2) || 0
        }
        return new b.Ellipse(d(g, e))
    };
    b.Ellipse.fromObject = function(e) {
        return new b.Ellipse(e)
    }
})(typeof exports != "undefined" ? exports : this);
(function(b) {
    var c = b.fabric || (b.fabric = {});
    if (c.Rect) {
        console.warn("fabric.Rect is already defined");
        return
    }
    c.Rect = c.util.createClass(c.Object, {type: "rect",options: {rx: 0,ry: 0},initialize: function(d) {
            this._initStateProperties();
            this.callSuper("initialize", d);
            this._initRxRy()
        },_initStateProperties: function() {
            this.stateProperties = this.stateProperties.concat(["rx", "ry"])
        },_initRxRy: function() {
            if (this.rx && !this.ry) {
                this.ry = this.rx
            } else {
                if (this.ry && !this.rx) {
                    this.rx = this.ry
                }
            }
        },_render: function(f) {
            var j = this.rx || 0, i = this.ry || 0, d = -this.width / 2, k = -this.height / 2, e = this.width, g = this.height;
            f.beginPath();
            f.globalAlpha *= this.opacity;
            if (this.group) {
                f.translate(this.x || 0, this.y || 0)
            }
            f.moveTo(d + j, k);
            f.lineTo(d + e - j, k);
            f.bezierCurveTo(d + e, k, d + e, k + i, d + e, k + i);
            f.lineTo(d + e, k + g - i);
            f.bezierCurveTo(d + e, k + g, d + e - j, k + g, d + e - j, k + g);
            f.lineTo(d + j, k + g);
            f.bezierCurveTo(d, k + g, d, k + g - i, d, k + g - i);
            f.lineTo(d, k + i);
            f.bezierCurveTo(d, k, d + j, k, d + j, k);
            f.closePath();
            if (this.fill) {
                f.fill()
            }
            if (this.stroke) {
                f.stroke()
            }
        },_normalizeLeftTopProperties: function(d) {
            if (d.left) {
                this.set("left", d.left + this.getWidth() / 2)
            }
            this.set("x", d.left || 0);
            if (d.top) {
                this.set("top", d.top + this.getHeight() / 2)
            }
            this.set("y", d.top || 0);
            return this
        },complexity: function() {
            return 1
        },toSVG: function() {
            return '<rect x="' + (-1 * this.width / 2) + '" y="' + (-1 * this.height / 2) + '" width="' + this.width + '" height="' + this.height + '" style="' + this.getSvgStyles() + '" transform="' + this.getSvgTransform() + '" />'
        }});
    c.Rect.ATTRIBUTE_NAMES = "x y width height rx ry fill fill-opacity opacity stroke stroke-width transform".split(" ");
    function a(d) {
        d.left = d.left || 0;
        d.top = d.top || 0;
        return d
    }
    c.Rect.fromElement = function(e, d) {
        if (!e) {
            return null
        }
        var g = c.parseAttributes(e, c.Rect.ATTRIBUTE_NAMES);
        g = a(g);
        var f = new c.Rect(c.util.object.extend((d ? c.util.object.clone(d) : {}), g));
        f._normalizeLeftTopProperties(g);
        return f
    };
    c.Rect.fromObject = function(d) {
        return new c.Rect(d)
    }
})(typeof exports != "undefined" ? exports : this);
(function(b) {
    var c = b.fabric || (b.fabric = {}), a = c.util.toFixed;
    if (c.Polyline) {
        c.warn("fabric.Polyline is already defined");
        return
    }
    c.Polyline = c.util.createClass(c.Object, {type: "polyline",initialize: function(e, d) {
            d = d || {};
            this.set("points", e);
            this.callSuper("initialize", d);
            this._calcDimensions()
        },_calcDimensions: function() {
            return c.Polygon.prototype._calcDimensions.call(this)
        },toObject: function() {
            return c.Polygon.prototype.toObject.call(this)
        },toSVG: function() {
            var f = [];
            for (var e = 0, d = this.points.length; e < d; e++) {
                f.push(a(this.points[e].x, 2), ",", a(this.points[e].y, 2), " ")
            }
            return ["<polyline ", 'points="', f.join(""), '" ', 'style="', this.getSvgStyles(), '" ', 'transform="', this.getSvgTransform(), '" ', "/>"].join("")
        },_render: function(f) {
            var e;
            f.beginPath();
            for (var g = 0, d = this.points.length; g < d; g++) {
                e = this.points[g];
                f.lineTo(e.x, e.y)
            }
            if (this.fill) {
                f.fill()
            }
            if (this.stroke) {
                f.stroke()
            }
        },complexity: function() {
            return this.get("points").length
        }});
    c.Polyline.ATTRIBUTE_NAMES = "fill fill-opacity opacity stroke stroke-width transform".split(" ");
    c.Polyline.fromElement = function(g, e) {
        if (!g) {
            return null
        }
        e || (e = {});
        var h = c.parsePointsAttribute(g.getAttribute("points")), j = c.parseAttributes(g, c.Polyline.ATTRIBUTE_NAMES);
        for (var f = 0, d = h.length; f < d; f++) {
            h[f].x -= (e.width / 2) || 0;
            h[f].y -= (e.height / 2) || 0
        }
        return new c.Polyline(h, c.util.object.extend(j, e))
    };
    c.Polyline.fromObject = function(d) {
        var e = d.points;
        return new c.Polyline(e, d)
    }
})(typeof exports != "undefined" ? exports : this);
(function(f) {
    var g = f.fabric || (f.fabric = {}), h = g.util.object.extend, e = g.util.array.min, b = g.util.array.max, a = g.util.toFixed;
    if (g.Polygon) {
        g.warn("fabric.Polygon is already defined");
        return
    }
    function d(i) {
        return i.x
    }
    function c(i) {
        return i.y
    }
    g.Polygon = g.util.createClass(g.Object, {type: "polygon",initialize: function(j, i) {
            i = i || {};
            this.points = j;
            this.callSuper("initialize", i);
            this._calcDimensions()
        },_calcDimensions: function() {
            var j = this.points, i = e(j, "x"), m = e(j, "y"), l = b(j, "x"), k = b(j, "y");
            this.width = l - i;
            this.height = k - m;
            this.minX = i;
            this.minY = m
        },toObject: function() {
            return h(this.callSuper("toObject"), {points: this.points.concat()})
        },toSVG: function() {
            var l = [];
            for (var k = 0, j = this.points.length; k < j; k++) {
                l.push(a(this.points[k].x, 2), ",", a(this.points[k].y, 2), " ")
            }
            return ["<polygon ", 'points="', l.join(""), '" ', 'style="', this.getSvgStyles(), '" ', 'transform="', this.getSvgTransform(), '" ', "/>"].join("")
        },_render: function(l) {
            var k;
            l.beginPath();
            for (var m = 0, j = this.points.length; m < j; m++) {
                k = this.points[m];
                l.lineTo(k.x, k.y)
            }
            if (this.fill) {
                l.fill()
            }
            if (this.stroke) {
                l.closePath();
                l.stroke()
            }
        },complexity: function() {
            return this.points.length
        }});
    g.Polygon.ATTRIBUTE_NAMES = "fill fill-opacity opacity stroke stroke-width transform".split(" ");
    g.Polygon.fromElement = function(m, k) {
        if (!m) {
            return null
        }
        k || (k = {});
        var n = g.parsePointsAttribute(m.getAttribute("points")), o = g.parseAttributes(m, g.Polygon.ATTRIBUTE_NAMES);
        for (var l = 0, j = n.length; l < j; l++) {
            n[l].x -= (k.width / 2) || 0;
            n[l].y -= (k.height / 2) || 0
        }
        return new g.Polygon(n, h(o, k))
    };
    g.Polygon.fromObject = function(i) {
        return new g.Polygon(i.points, i)
    }
})(typeof exports != "undefined" ? exports : this);
(function(a) {
    var l = {m: 2,l: 2,h: 1,v: 1,c: 6,s: 4,q: 4,t: 2,a: 7};
    function k(F, B, z, A) {
        var s = A[0];
        var q = A[1];
        var t = A[2];
        var D = A[3];
        var E = A[4];
        var w = A[5];
        var v = A[6];
        var r = i(w, v, s, q, D, E, t, B, z);
        for (var u = 0; u < r.length; u++) {
            var C = f.apply(this, r[u]);
            F.bezierCurveTo.apply(F, C)
        }
    }
    var b = {}, p = {}, o = Array.prototype.join, j;
    function i(T, S, O, N, J, P, G, M, L) {
        j = o.call(arguments);
        if (b[j]) {
            return b[j]
        }
        var r = G * (Math.PI / 180);
        var W = Math.sin(r);
        var aa = Math.cos(r);
        O = Math.abs(O);
        N = Math.abs(N);
        var A = aa * (M - T) * 0.5 + W * (L - S) * 0.5;
        var z = aa * (L - S) * 0.5 - W * (M - T) * 0.5;
        var E = (A * A) / (O * O) + (z * z) / (N * N);
        if (E > 1) {
            E = Math.sqrt(E);
            O *= E;
            N *= E
        }
        var R = aa / O;
        var Q = W / O;
        var D = (-W) / N;
        var C = (aa) / N;
        var H = R * M + Q * L;
        var q = D * M + C * L;
        var F = R * T + Q * S;
        var ab = D * T + C * S;
        var Z = (F - H) * (F - H) + (ab - q) * (ab - q);
        var U = 1 / Z - 0.25;
        if (U < 0) {
            U = 0
        }
        var w = Math.sqrt(U);
        if (P == J) {
            w = -w
        }
        var V = 0.5 * (H + F) - w * (ab - q);
        var K = 0.5 * (q + ab) + w * (F - H);
        var v = Math.atan2(q - K, H - V);
        var u = Math.atan2(ab - K, F - V);
        var Y = u - v;
        if (Y < 0 && P == 1) {
            Y += 2 * Math.PI
        } else {
            if (Y > 0 && P == 0) {
                Y -= 2 * Math.PI
            }
        }
        var B = Math.ceil(Math.abs(Y / (Math.PI * 0.5 + 0.001)));
        var I = [];
        for (var X = 0; X < B; X++) {
            var t = v + X * Y / B;
            var s = v + (X + 1) * Y / B;
            I[X] = [V, K, t, s, O, N, W, aa]
        }
        return (b[j] = I)
    }
    function f(w, s, E, C, y, x, u, D) {
        j = o.call(arguments);
        if (p[j]) {
            return p[j]
        }
        var K = D * y;
        var J = -u * x;
        var A = u * y;
        var z = D * x;
        var F = 0.5 * (C - E);
        var B = (8 / 3) * Math.sin(F * 0.5) * Math.sin(F * 0.5) / Math.sin(F);
        var I = w + Math.cos(E) - B * Math.sin(E);
        var v = s + Math.sin(E) + B * Math.cos(E);
        var G = w + Math.cos(C);
        var q = s + Math.sin(C);
        var H = G + B * Math.sin(C);
        var r = q - B * Math.cos(C);
        return (p[j] = [K * I + J * v, A * I + z * v, K * H + J * r, A * H + z * r, K * G + J * q, A * G + z * q])
    }
    "use strict";
    var d = a.fabric || (a.fabric = {}), e = d.util.array.min, h = d.util.array.max, g = d.util.object.extend, c = Object.prototype.toString;
    if (d.Path) {
        d.warn("fabric.Path is already defined");
        return
    }
    if (!d.Object) {
        d.warn("fabric.Path requires fabric.Object");
        return
    }
    function n(q) {
        if (q[0] === "H") {
            return q[1]
        }
        return q[q.length - 2]
    }
    function m(q) {
        if (q[0] === "V") {
            return q[1]
        }
        return q[q.length - 1]
    }
    d.Path = d.util.createClass(d.Object, {type: "path",initialize: function(s, r) {
            r = r || {};
            this.setOptions(r);
            if (!s) {
                throw Error("`path` argument is required")
            }
            var q = c.call(s) === "[object Array]";
            this.path = q ? s : s.match && s.match(/[a-zA-Z][^a-zA-Z]*/g);
            if (!this.path) {
                return
            }
            if (!q) {
                this._initializeFromArray(r)
            }
            if (r.sourcePath) {
                this.setSourcePath(r.sourcePath)
            }
        },_initializeFromArray: function(r) {
            var q = "width" in r, s = "height" in r;
            this.path = this._parsePath();
            if (!q || !s) {
                g(this, this._parseDimensions());
                if (q) {
                    this.width = r.width
                }
                if (s) {
                    this.height = r.height
                }
            }
        },_render: function(D) {
            var z, B = 0, A = 0, q = 0, E = 0, v, u, r = -(this.width / 2), C = -(this.height / 2);
            for (var s = 0, w = this.path.length; s < w; ++s) {
                z = this.path[s];
                switch (z[0]) {
                    case "l":
                        B += z[1];
                        A += z[2];
                        D.lineTo(B + r, A + C);
                        break;
                    case "L":
                        B = z[1];
                        A = z[2];
                        D.lineTo(B + r, A + C);
                        break;
                    case "h":
                        B += z[1];
                        D.lineTo(B + r, A + C);
                        break;
                    case "H":
                        B = z[1];
                        D.lineTo(B + r, A + C);
                        break;
                    case "v":
                        A += z[1];
                        D.lineTo(B + r, A + C);
                        break;
                    case "V":
                        A = z[1];
                        D.lineTo(B + r, A + C);
                        break;
                    case "m":
                        B += z[1];
                        A += z[2];
                        D.moveTo(B + r, A + C);
                        break;
                    case "M":
                        B = z[1];
                        A = z[2];
                        D.moveTo(B + r, A + C);
                        break;
                    case "c":
                        v = B + z[5];
                        u = A + z[6];
                        q = B + z[3];
                        E = A + z[4];
                        D.bezierCurveTo(B + z[1] + r, A + z[2] + C, q + r, E + C, v + r, u + C);
                        B = v;
                        A = u;
                        break;
                    case "C":
                        B = z[5];
                        A = z[6];
                        q = z[3];
                        E = z[4];
                        D.bezierCurveTo(z[1] + r, z[2] + C, q + r, E + C, B + r, A + C);
                        break;
                    case "s":
                        v = B + z[3];
                        u = A + z[4];
                        q = 2 * B - q;
                        E = 2 * A - E;
                        D.bezierCurveTo(q + r, E + C, B + z[1] + r, A + z[2] + C, v + r, u + C);
                        B = v;
                        A = u;
                        break;
                    case "S":
                        v = z[3];
                        u = z[4];
                        q = 2 * B - q;
                        E = 2 * A - E;
                        D.bezierCurveTo(q + r, E + C, z[1] + r, z[2] + C, v + r, u + C);
                        B = v;
                        A = u;
                        break;
                    case "q":
                        B += z[3];
                        A += z[4];
                        D.quadraticCurveTo(z[1] + r, z[2] + C, B + r, A + C);
                        break;
                    case "Q":
                        B = z[3];
                        A = z[4];
                        q = z[1];
                        E = z[2];
                        D.quadraticCurveTo(q + r, E + C, B + r, A + C);
                        break;
                    case "T":
                        v = B;
                        u = A;
                        B = z[1];
                        A = z[2];
                        q = -q + 2 * v;
                        E = -E + 2 * u;
                        D.quadraticCurveTo(q + r, E + C, B + r, A + C);
                        break;
                    case "a":
                        k(D, B + r, A + C, [z[1], z[2], z[3], z[4], z[5], z[6] + B + r, z[7] + A + C]);
                        B += z[6];
                        A += z[7];
                        break;
                    case "A":
                        k(D, B + r, A + C, [z[1], z[2], z[3], z[4], z[5], z[6] + r, z[7] + C]);
                        B = z[6];
                        A = z[7];
                        break;
                    case "z":
                    case "Z":
                        D.closePath();
                        break
                }
            }
        },render: function(r, s) {
            r.save();
            var q = this.transformMatrix;
            if (q) {
                r.transform(q[0], q[1], q[2], q[3], q[4], q[5])
            }
            if (!s) {
                this.transform(r)
            }
            if (this.overlayFill) {
                r.fillStyle = this.overlayFill
            } else {
                if (this.fill) {
                    r.fillStyle = this.fill
                }
            }
            if (this.stroke) {
                r.strokeStyle = this.stroke
            }
            r.beginPath();
            this._render(r);
            if (this.fill) {
                r.fill()
            }
            if (this.stroke) {
                r.strokeStyle = this.stroke;
                r.lineWidth = this.strokeWidth;
                r.lineCap = r.lineJoin = "round";
                r.stroke()
            }
            if (!s && this.active) {
                this.drawBorders(r);
                this.hideCorners || this.drawCorners(r)
            }
            r.restore()
        },toString: function() {
            return "#<fabric.Path (" + this.complexity() + '): { "top": ' + this.top + ', "left": ' + this.left + " }>"
        },toObject: function() {
            var q = g(this.callSuper("toObject"), {path: this.path});
            if (this.sourcePath) {
                q.sourcePath = this.sourcePath
            }
            if (this.transformMatrix) {
                q.transformMatrix = this.transformMatrix
            }
            return q
        },toDatalessObject: function() {
            var q = this.toObject();
            if (this.sourcePath) {
                q.path = this.sourcePath
            }
            delete q.sourcePath;
            return q
        },toSVG: function() {
            var t = [];
            for (var r = 0, q = this.path.length; r < q; r++) {
                t.push(this.path[r].join(" "))
            }
            var s = t.join(" ");
            return ['<g transform="', this.getSvgTransform(), '">', "<path ", 'width="', this.width, '" height="', this.height, '" ', 'd="', s, '" ', 'style="', this.getSvgStyles(), '" ', 'transform="translate(', (-this.width / 2), " ", (-this.height / 2), ')" />', "</g>"].join("")
        },complexity: function() {
            return this.path.length
        },_parsePath: function() {
            var C = [], z, v, y;
            for (var u = 0, t, q, w = this.path.length; u < w; u++) {
                z = this.path[u];
                v = z.slice(1).trim().replace(/(\d)-/g, "$1###-").split(/\s|,|###/);
                q = [z.charAt(0)];
                for (var t = 0, A = v.length; t < A; t++) {
                    y = parseFloat(v[t]);
                    if (!isNaN(y)) {
                        q.push(y)
                    }
                }
                var s = q[0].toLowerCase(), B = l[s];
                if (q.length - 1 > B) {
                    for (var r = 1, x = q.length; r < x; r += B) {
                        C.push([s].concat(q.slice(r, r + B)))
                    }
                } else {
                    C.push(q)
                }
            }
            return C
        },_parseDimensions: function() {
            var z = [], v = [], A, w, B = false, D, C;
            this.path.forEach(function(y, x) {
                if (y[0] !== "H") {
                    A = (x === 0) ? n(y) : n(this.path[x - 1])
                }
                if (y[0] !== "V") {
                    w = (x === 0) ? m(y) : m(this.path[x - 1])
                }
                if (y[0] === y[0].toLowerCase()) {
                    B = true
                }
                D = B ? A + n(y) : y[0] === "V" ? A : n(y);
                C = B ? w + m(y) : y[0] === "H" ? w : m(y);
                var E = parseInt(D, 10);
                if (!isNaN(E)) {
                    z.push(E)
                }
                E = parseInt(C, 10);
                if (!isNaN(E)) {
                    v.push(E)
                }
            }, this);
            var s = e(z), r = e(v), u = 0, t = 0;
            var q = {top: r - t,left: s - u,bottom: h(v) - t,right: h(z) - u};
            q.width = q.right - q.left;
            q.height = q.bottom - q.top;
            return q
        }});
    d.Path.fromObject = function(q) {
        return new d.Path(q.path, q)
    };
    d.Path.ATTRIBUTE_NAMES = "d fill fill-opacity opacity fill-rule stroke stroke-width transform".split(" ");
    d.Path.fromElement = function(r, q) {
        var s = d.parseAttributes(r, d.Path.ATTRIBUTE_NAMES);
        return new d.Path(s.d, g(s, q))
    }
})(typeof exports != "undefined" ? exports : this);
(function(a) {
    var d = a.fabric || (a.fabric = {}), h = d.util.object.extend, f = d.util.array.invoke, i = d.Object.prototype.set, e = d.Object.prototype.toObject, b = d.util.string.camelize, g = d.util.string.capitalize;
    if (d.PathGroup) {
        d.warn("fabric.PathGroup is already defined");
        return
    }
    d.PathGroup = d.util.createClass(d.Path, {type: "path-group",forceFillOverwrite: false,initialize: function(l, j) {
            j = j || {};
            this.paths = l || [];
            for (var k = this.paths.length; k--; ) {
                this.paths[k].group = this
            }
            this.setOptions(j);
            this.setCoords();
            if (j.sourcePath) {
                this.setSourcePath(j.sourcePath)
            }
        },render: function(n) {
            if (this.stub) {
                n.save();
                this.transform(n);
                this.stub.render(n, false);
                if (this.active) {
                    this.drawBorders(n);
                    this.drawCorners(n)
                }
                n.restore()
            } else {
                n.save();
                var j = this.transformMatrix;
                if (j) {
                    n.transform(j[0], j[1], j[2], j[3], j[4], j[5])
                }
                this.transform(n);
                for (var o = 0, k = this.paths.length; o < k; ++o) {
                    this.paths[o].render(n, true)
                }
                if (this.active) {
                    this.drawBorders(n);
                    this.hideCorners || this.drawCorners(n)
                }
                n.restore()
            }
        },set: function(l, k) {
            if ((l === "fill" || l === "overlayFill") && this.isSameColor()) {
                this[l] = k;
                var j = this.paths.length;
                while (j--) {
                    this.paths[j].set(l, k)
                }
            } else {
                i.call(this, l, k)
            }
            return this
        },toObject: function() {
            return h(e.call(this), {paths: f(this.getObjects(), "clone"),sourcePath: this.sourcePath})
        },toDatalessObject: function() {
            var j = this.toObject();
            if (this.sourcePath) {
                j.paths = this.sourcePath
            }
            return j
        },toSVG: function() {
            var m = this.getObjects();
            var k = ["<g ", 'width="', this.width, '" ', 'height="', this.height, '" ', 'style="', this.getSvgStyles(), '" ', 'transform="', this.getSvgTransform(), '" ', ">"];
            for (var l = 0, j = m.length; l < j; l++) {
                k.push(m[l].toSVG())
            }
            k.push("</g>");
            return k.join("")
        },toString: function() {
            return "#<fabric.PathGroup (" + this.complexity() + "): { top: " + this.top + ", left: " + this.left + " }>"
        },isSameColor: function() {
            var j = this.getObjects()[0].get("fill");
            return this.getObjects().every(function(k) {
                return k.get("fill") === j
            })
        },complexity: function() {
            return this.paths.reduce(function(j, k) {
                return j + ((k && k.complexity) ? k.complexity() : 0)
            }, 0)
        },toGrayscale: function() {
            var j = this.paths.length;
            while (j--) {
                this.paths[j].toGrayscale()
            }
            return this
        },getObjects: function() {
            return this.paths
        }});
    function c(m) {
        for (var l = 0, j = m.length; l < j; l++) {
            if (!(m[l] instanceof d.Object)) {
                var k = b(g(m[l].type));
                m[l] = d[k].fromObject(m[l])
            }
        }
        return m
    }
    d.PathGroup.fromObject = function(j) {
        var k = c(j.paths);
        return new d.PathGroup(k, j)
    }
})(typeof exports != "undefined" ? exports : this);
(function(d) {
    var e = d.fabric || (d.fabric = {}), g = e.util.object.extend, c = e.util.array.min, a = e.util.array.max, b = e.util.array.invoke, f = e.util.removeFromArray;
    if (e.Group) {
        return
    }
    e.Group = e.util.createClass(e.Object, {type: "group",initialize: function(i, h) {
            this.objects = i || [];
            this.originalState = {};
            this.callSuper("initialize");
            this._calcBounds();
            this._updateObjectsCoords();
            if (h) {
                g(this, h)
            }
            this._setOpacityIfSame();
            this.setCoords(true);
            this.saveCoords();
            this.activateAllObjects()
        },_updateObjectsCoords: function() {
            var i = this.left, h = this.top;
            this.forEachObject(function(j) {
                var k = j.get("left"), l = j.get("top");
                j.set("originalLeft", k);
                j.set("originalTop", l);
                j.set("left", k - i);
                j.set("top", l - h);
                j.setCoords();
                j.hideCorners = true
            }, this)
        },toString: function() {
            return "#<fabric.Group: (" + this.complexity() + ")>"
        },getObjects: function() {
            return this.objects
        },add: function(h) {
            this._restoreObjectsState();
            this.objects.push(h);
            h.setActive(true);
            this._calcBounds();
            this._updateObjectsCoords();
            return this
        },remove: function(h) {
            this._restoreObjectsState();
            f(this.objects, h);
            h.setActive(false);
            this._calcBounds();
            this._updateObjectsCoords();
            return this
        },size: function() {
            return this.getObjects().length
        },set: function(h, k) {
            if (typeof k == "function") {
                this.set(h, k(this[h]))
            } else {
                if (h === "fill" || h === "opacity") {
                    var j = this.objects.length;
                    this[h] = k;
                    while (j--) {
                        this.objects[j].set(h, k)
                    }
                } else {
                    this[h] = k
                }
            }
            return this
        },contains: function(h) {
            return this.objects.indexOf(h) > -1
        },toObject: function() {
            return g(this.callSuper("toObject"), {objects: b(this.objects, "clone")})
        },render: function(j) {
            j.save();
            this.transform(j);
            var n = Math.max(this.scaleX, this.scaleY);
            for (var l = 0, h = this.objects.length, k; k = this.objects[l]; l++) {
                var m = k.borderScaleFactor;
                k.borderScaleFactor = n;
                k.render(j);
                k.borderScaleFactor = m
            }
            this.hideBorders || this.drawBorders(j);
            this.hideCorners || this.drawCorners(j);
            j.restore();
            this.setCoords()
        },item: function(h) {
            return this.getObjects()[h]
        },complexity: function() {
            return this.getObjects().reduce(function(i, h) {
                i += (typeof h.complexity == "function") ? h.complexity() : 0;
                return i
            }, 0)
        },_restoreObjectsState: function() {
            this.objects.forEach(this._restoreObjectState, this);
            return this
        },_restoreObjectState: function(j) {
            var l = this.get("left"), m = this.get("top"), h = this.getAngle() * (Math.PI / 180), k = j.get("originalLeft"), n = j.get("originalTop"), i = Math.cos(h) * j.get("top") + Math.sin(h) * j.get("left"), o = -Math.sin(h) * j.get("top") + Math.cos(h) * j.get("left");
            j.setAngle(j.getAngle() + this.getAngle());
            j.set("left", l + o * this.get("scaleX"));
            j.set("top", m + i * this.get("scaleY"));
            j.set("scaleX", j.get("scaleX") * this.get("scaleX"));
            j.set("scaleY", j.get("scaleY") * this.get("scaleY"));
            j.setCoords();
            j.hideCorners = false;
            j.setActive(false);
            j.setCoords();
            return this
        },destroy: function() {
            return this._restoreObjectsState()
        },saveCoords: function() {
            this._originalLeft = this.get("left");
            this._originalTop = this.get("top");
            return this
        },hasMoved: function() {
            return this._originalLeft !== this.get("left") || this._originalTop !== this.get("top")
        },setObjectsCoords: function() {
            this.forEachObject(function(h) {
                h.setCoords()
            });
            return this
        },activateAllObjects: function() {
            return this.setActive(true)
        },setActive: function(h) {
            this.forEachObject(function(i) {
                i.setActive(h)
            });
            return this
        },forEachObject: e.StaticCanvas.prototype.forEachObject,_setOpacityIfSame: function() {
            var j = this.getObjects(), i = j[0] ? j[0].get("opacity") : 1;
            var h = j.every(function(k) {
                return k.get("opacity") === i
            });
            if (h) {
                this.opacity = i
            }
        },_calcBounds: function() {
            var s = [], q = [], p, n, k, h, m, l, u, r = 0, t = this.objects.length;
            for (; r < t; ++r) {
                m = this.objects[r];
                m.setCoords();
                for (var j in m.oCoords) {
                    s.push(m.oCoords[j].x);
                    q.push(m.oCoords[j].y)
                }
            }
            p = c(s);
            k = a(s);
            n = c(q);
            h = a(q);
            l = (k - p) || 0;
            u = (h - n) || 0;
            this.width = l;
            this.height = u;
            this.left = (p + l / 2) || 0;
            this.top = (n + u / 2) || 0
        },containsPoint: function(i) {
            var l = this.get("width") / 2, h = this.get("height") / 2, k = this.get("left"), j = this.get("top");
            return k - l < i.x && k + l > i.x && j - h < i.y && j + h > i.y
        },toGrayscale: function() {
            var h = this.objects.length;
            while (h--) {
                this.objects[h].toGrayscale()
            }
        }});
    e.Group.fromObject = function(h) {
        return new e.Group(h.objects, h)
    }
})(typeof exports != "undefined" ? exports : this);
(function(a) {
    var b = fabric.util.object.extend;
    if (!a.fabric) {
        a.fabric = {}
    }
    if (a.fabric.Image) {
        fabric.warn("fabric.Image is already defined.");
        return
    }
    if (!fabric.Object) {
        fabric.warn("fabric.Object is required for fabric.Image initialization");
        return
    }
    fabric.Image = fabric.util.createClass(fabric.Object, {active: false,bordervisibility: false,cornervisibility: false,type: "image",filters: [],initialize: function(d, c) {
            c || (c = {});
            this.callSuper("initialize", c);
            this._initElement(d);
            this._originalImage = this.getElement();
            this._initConfig(c);
            if (c.filters) {
                this.filters = c.filters;
                this.applyFilters()
            }
        },getElement: function() {
            return this._element
        },setElement: function(c) {
            this._element = c;
            this._initConfig();
            return this
        },getOriginalSize: function() {
            var c = this.getElement();
            return {width: c.width,height: c.height}
        },setBorderVisibility: function(c) {
            this._resetWidthHeight();
            this._adjustWidthHeightToBorders(showBorder);
            this.setCoords()
        },setCornersVisibility: function(c) {
            this.cornervisibility = !!c
        },render: function(c, d) {
            c.save();
            if (!d) {
                this.transform(c)
            }
            this._render(c);
            if (this.active && !d) {
                this.drawBorders(c);
                this.hideCorners || this.drawCorners(c)
            }
            c.restore()
        },toObject: function() {
            return b(this.callSuper("toObject"), {src: this._originalImage.src || this._originalImage._src,filters: this.filters.concat()})
        },toSVG: function() {
            return '<g transform="' + this.getSvgTransform() + '"><image xlink:href="' + this.getSvgSrc() + '" style="' + this.getSvgStyles() + '" transform="translate(' + (-this.width / 2) + " " + (-this.height / 2) + ')" width="' + this.width + '" height="' + this.height + '"/></g>'
        },getSrc: function() {
            return this.getElement().src || this.getElement()._src
        },toString: function() {
            return '#<fabric.Image: { src: "' + this.getSrc() + '" }>'
        },clone: function(c) {
            this.constructor.fromObject(this.toObject(), c)
        },applyFilters: function(i) {
            if (this.filters.length === 0) {
                this.setElement(this._originalImage);
                i && i();
                return
            }
            var c = typeof Buffer !== "undefined" && typeof window === "undefined", g = this._originalImage, e = fabric.document.createElement("canvas"), f = c ? new (require("canvas").Image) : fabric.document.createElement("img"), h = this;
            if (!e.getContext && typeof G_vmlCanvasManager != "undefined") {
                G_vmlCanvasManager.initElement(e)
            }
            e.width = g.width;
            e.height = g.height;
            e.getContext("2d").drawImage(g, 0, 0);
            this.filters.forEach(function(j) {
                j && j.applyTo(e)
            });
            f.onload = function() {
                h.setElement(f);
                i && i();
                f.onload = e = g = null
            };
            f.width = g.width;
            f.height = g.height;
            if (c) {
                var d = e.toDataURL("image/png").replace(/data:image\/png;base64,/, "");
                f.src = new Buffer(d, "base64");
                h.setElement(f);
                i && i()
            } else {
                f.src = e.toDataURL("image/png")
            }
            return this
        },_render: function(d) {
            var c = this.getOriginalSize();
            d.drawImage(this.getElement(), -c.width / 2, -c.height / 2, c.width, c.height)
        },_adjustWidthHeightToBorders: function(c) {
            if (c) {
                this.currentBorder = this.borderwidth;
                this.width += (2 * this.currentBorder);
                this.height += (2 * this.currentBorder)
            } else {
                this.currentBorder = 0
            }
        },_resetWidthHeight: function() {
            var c = this.getElement();
            this.set("width", c.width);
            this.set("height", c.height)
        },_initElement: function(c) {
            this.setElement(fabric.util.getById(c));
            fabric.util.addClass(this.getElement(), fabric.Image.CSS_CANVAS)
        },_initConfig: function(c) {
            this.setOptions(c || {});
            this._setBorder();
            this._setWidthHeight()
        },_initFilters: function(c) {
            if (c.filters && c.filters.length) {
                this.filters = c.filters.map(function(d) {
                    return fabric.Image.filters[d.type].fromObject(d)
                })
            }
        },_setBorder: function() {
            if (this.bordervisibility) {
                this.currentBorder = this.borderwidth
            } else {
                this.currentBorder = 0
            }
        },_setWidthHeight: function() {
            var c = 2 * this.currentBorder;
            this.width = (this.getElement().width || 0) + c;
            this.height = (this.getElement().height || 0) + c
        },complexity: function() {
            return 1
        }});
    fabric.Image.CSS_CANVAS = "canvas-img";
    fabric.Image.prototype.getSvgSrc = fabric.Image.prototype.getSrc;
    fabric.Image.fromObject = function(d, f) {
        var c = fabric.document.createElement("img"), e = d.src;
        if (d.width) {
            c.width = d.width
        }
        if (d.height) {
            c.height = d.height
        }
        c.onload = function() {
            fabric.Image.prototype._initFilters.call(d, d);
            var g = new fabric.Image(c, d);
            f && f(g);
            c = c.onload = null
        };
        c.src = e
    };
    fabric.Image.fromURL = function(d, f, e) {
        var c = fabric.document.createElement("img");
        c.onload = function() {
            if (f) {
                f(new fabric.Image(c, e))
            }
            c = c.onload = null
        };
        c.src = d
    };
    fabric.Image.ATTRIBUTE_NAMES = "x y width height fill fill-opacity opacity stroke stroke-width transform xlink:href".split(" ");
    fabric.Image.fromElement = function(d, f, c) {
        c || (c = {});
        var e = fabric.parseAttributes(d, fabric.Image.ATTRIBUTE_NAMES);
        fabric.Image.fromURL(e["xlink:href"], f, b(e, c))
    };
    fabric.Image.async = true
})(typeof exports != "undefined" ? exports : this);
fabric.Image.filters = {};
fabric.Image.filters.Grayscale = fabric.util.createClass({type: "Grayscale",applyTo: function(d) {
        var c = d.getContext("2d"), a = c.getImageData(0, 0, d.width, d.height), h = a.data, e = a.width, l = a.height, k, b, g, f;
        for (g = 0; g < e; g++) {
            for (f = 0; f < l; f++) {
                k = (g * 4) * l + (f * 4);
                b = (h[k] + h[k + 1] + h[k + 2]) / 3;
                h[k] = b;
                h[k + 1] = b;
                h[k + 2] = b
            }
        }
        c.putImageData(a, 0, 0)
    },toJSON: function() {
        return {type: this.type}
    }});
fabric.Image.filters.Grayscale.fromObject = function() {
    return new fabric.Image.filters.Grayscale()
};
fabric.Image.filters.RemoveWhite = fabric.util.createClass({type: "RemoveWhite",initialize: function(a) {
        a || (a = {});
        this.threshold = a.threshold || 30;
        this.distance = a.distance || 20
    },applyTo: function(f) {
        var e = f.getContext("2d"), c = e.getImageData(0, 0, f.width, f.height), k = c.data, l = this.threshold, d = this.distance, h = 255 - l, p = Math.abs, a, m, o;
        for (var j = 0, n = k.length; j < n; j += 4) {
            a = k[j];
            m = k[j + 1];
            o = k[j + 2];
            if (a > h && m > h && o > h && p(a - m) < d && p(a - o) < d && p(m - o) < d) {
                k[j + 3] = 1
            }
        }
        e.putImageData(c, 0, 0)
    },toJSON: function() {
        return {type: this.type,threshold: this.threshold,distance: this.distance}
    }});
fabric.Image.filters.RemoveWhite.fromObject = function(a) {
    return new fabric.Image.filters.RemoveWhite(a)
};
fabric.Image.filters.Invert = fabric.util.createClass({type: "Invert",applyTo: function(d) {
        var c = d.getContext("2d"), f = c.getImageData(0, 0, d.width, d.height), e = f.data, a = e.length, b;
        for (b = 0; b < a; b += 4) {
            e[b] = 255 - e[b];
            e[b + 1] = 255 - e[b + 1];
            e[b + 2] = 255 - e[b + 2]
        }
        c.putImageData(f, 0, 0)
    },toJSON: function() {
        return {type: this.type}
    }});
fabric.Image.filters.Invert.fromObject = function() {
    return new fabric.Image.filters.Invert()
};
(function(b) {
    var c = b.fabric || (b.fabric = {}), e = c.util.object.extend, d = c.util.object.clone, a = c.util.toFixed;
    if (c.Text) {
        c.warn("fabric.Text is already defined");
        return
    }
    if (!c.Object) {
        c.warn("fabric.Text requires fabric.Object");
        return
    }
    c.Text = c.util.createClass(c.Object, {fontSize: 40,fontWeight: 100,fontFamily: "Times_New_Roman",textDecoration: "",textShadow: null,textAlign: "left",fontStyle: "",lineHeight: 1.6,strokeStyle: "",strokeWidth: 1,backgroundColor: "",path: null,type: "text",initialize: function(g, f) {
            this._initStateProperties();
            this.text = g;
            this.setOptions(f);
            this.theta = this.angle * Math.PI / 180;
            this.width = this.getWidth();
            this.setCoords()
        },_initStateProperties: function() {
            this.stateProperties = this.stateProperties.concat();
            this.stateProperties.push("fontFamily", "fontWeight", "fontSize", "path", "text", "textDecoration", "textShadow", "textAlign", "fontStyle", "lineHeight", "strokeStyle", "strokeWidth", "backgroundColor");
            c.util.removeFromArray(this.stateProperties, "width")
        },toString: function() {
            return "#<fabric.Text (" + this.complexity() + '): { "text": "' + this.text + '", "fontFamily": "' + this.fontFamily + '" }>'
        },_render: function(f) {
            var h = Cufon.textOptions || (Cufon.textOptions = {});
            h.left = this.left;
            h.top = this.top;
            h.context = f;
            h.color = this.fill;
            var g = this._initDummyElement();
            this.transform(f);
            Cufon.replaceElement(g, {engine: "canvas",separate: "none",fontFamily: this.fontFamily,fontWeight: this.fontWeight,textDecoration: this.textDecoration,textShadow: this.textShadow,textAlign: this.textAlign,fontStyle: this.fontStyle,lineHeight: this.lineHeight,strokeStyle: this.strokeStyle,strokeWidth: this.strokeWidth,backgroundColor: this.backgroundColor});
            this.width = h.width;
            this.height = h.height;
            this._totalLineHeight = h.totalLineHeight;
            this._fontAscent = h.fontAscent;
            this._boundaries = h.boundaries;
            this._shadowOffsets = h.shadowOffsets;
            this._shadows = h.shadows || [];
            this.setCoords()
        },_initDummyElement: function() {
            var g = c.document.createElement("pre"), f = c.document.createElement("div");
            f.appendChild(g);
            if (typeof G_vmlCanvasManager == "undefined") {
                g.innerHTML = this.text
            } else {
                g.innerText = this.text.replace(/\r?\n/gi, "\r")
            }
            g.style.fontSize = this.fontSize + "px";
            g.style.letterSpacing = "normal";
            return g
        },render: function(f, g) {
            f.save();
            this._render(f);
            if (!g && this.active) {
                this.drawBorders(f);
                this.hideCorners || this.drawCorners(f)
            }
            f.restore()
        },toObject: function() {
            return e(this.callSuper("toObject"), {text: this.text,fontSize: this.fontSize,fontWeight: this.fontWeight,fontFamily: this.fontFamily,fontStyle: this.fontStyle,lineHeight: this.lineHeight,textDecoration: this.textDecoration,textShadow: this.textShadow,textAlign: this.textAlign,path: this.path,strokeStyle: this.strokeStyle,strokeWidth: this.strokeWidth,backgroundColor: this.backgroundColor})
        },toSVG: function() {
            var i = this.text.split(/\r?\n/), j = -this._fontAscent - ((this._fontAscent / 5) * this.lineHeight), g = -(this.width / 2), f = (this.height / 2) - (i.length * this.fontSize) - this._totalLineHeight, h = this._getSVGTextAndBg(j, g, i), k = this._getSVGShadows(j, i);
            f += ((this._fontAscent / 5) * this.lineHeight);
            return ['<g transform="', this.getSvgTransform(), '">', h.textBgRects.join(""), "<text ", (this.fontFamily ? "font-family=\"'" + this.fontFamily + "'\" " : ""), (this.fontSize ? 'font-size="' + this.fontSize + '" ' : ""), (this.fontStyle ? 'font-style="' + this.fontStyle + '" ' : ""), (this.fontWeight ? 'font-weight="' + this.fontWeight + '" ' : ""), (this.textDecoration ? 'text-decoration="' + this.textDecoration + '" ' : ""), 'style="', this.getSvgStyles(), '" ', 'transform="translate(', a(g, 2), " ", a(f, 2), ')">', k.join(""), h.textSpans.join(""), "</text>", "</g>"].join("")
        },_getSVGShadows: function(h, n) {
            var o = [], k, l, p, g, f = 1;
            for (k = 0, p = this._shadows.length; k < p; k++) {
                for (l = 0, g = n.length; l < g; l++) {
                    if (n[l] !== "") {
                        var m = (this._boundaries && this._boundaries[l]) ? this._boundaries[l].left : 0;
                        o.push('<tspan x="', a((m + f) + this._shadowOffsets[k][0], 2), (l === 0 ? '" y' : '" dy'), '="', a(h + (l === 0 ? this._shadowOffsets[k][1] : 0), 2), '" ', this._getFillAttributes(this._shadows[k].color), ">", c.util.string.escapeXml(n[l]), "</tspan>");
                        f = 1
                    } else {
                        f++
                    }
                }
            }
            return o
        },_getSVGTextAndBg: function(j, h, n) {
            var g = [], k = [], l, m, o, f = 1;
            for (l = 0, o = n.length; l < o; l++) {
                if (n[l] !== "") {
                    m = (this._boundaries && this._boundaries[l]) ? a(this._boundaries[l].left, 2) : 0;
                    g.push('<tspan x="', m, '" ', (l === 0 ? "y" : "dy"), '="', a(j * f, 2), '" ', this._getFillAttributes(this.fill), ">", c.util.string.escapeXml(n[l]), "</tspan>");
                    f = 1
                } else {
                    f++
                }
                if (!this.backgroundColor) {
                    continue
                }
                k.push("<rect ", this._getFillAttributes(this.backgroundColor), ' x="', a(h + this._boundaries[l].left, 2), '" y="', a((j * l) - this.height / 2, 2), '" width="', a(this._boundaries[l].width, 2), '" height="', a(this._boundaries[l].height, 2), '"></rect>')
            }
            return {textSpans: g,textBgRects: k}
        },_getFillAttributes: function(f) {
            var g = f ? new c.Color(f) : "";
            if (!g || !g.getSource() || g.getAlpha() === 1) {
                return 'fill="' + f + '"'
            }
            return 'opacity="' + g.getAlpha() + '" fill="' + g.setAlpha(1).toRgb() + '"'
        },setColor: function(f) {
            this.set("fill", f);
            return this
        },setFontsize: function(f) {
            this.set("fontSize", f);
            this.setCoords();
            return this
        },getText: function() {
            return this.text
        },setText: function(f) {
            this.set("text", f);
            this.setCoords();
            return this
        },set: function(f, g) {
            if (typeof f == "object") {
                for (var h in f) {
                    this.set(h, f[h])
                }
            } else {
                this[f] = g;
                if (f === "fontFamily" && this.path) {
                    this.path = this.path.replace(/(.*?)([^\/]*)(\.font\.js)/, "$1" + g + "$3")
                }
            }
            return this
        }});
    c.Text.fromObject = function(f) {
        return new c.Text(f.text, d(f))
    };
    c.Text.fromElement = function(f) {
    }
})(typeof exports != "undefined" ? exports : this);
(function() {
    if (typeof document != "undefined" && typeof window != "undefined") {
        return
    }
    var XML = require("o3-xml"), URL = require("url"), HTTP = require("http"), Canvas = require("canvas"), Image = require("canvas").Image;
    function request(url, encoding, callback) {
        var oURL = URL.parse(url), client = HTTP.createClient(80, oURL.hostname), request = client.request("GET", oURL.pathname, {host: oURL.hostname});
        client.addListener("error", function(err) {
            if (err.errno === process.ECONNREFUSED) {
                fabric.log("ECONNREFUSED: connection refused to " + client.host + ":" + client.port)
            } else {
                fabric.log(err.message)
            }
        });
        request.end();
        request.on("response", function(response) {
            var body = "";
            if (encoding) {
                response.setEncoding(encoding)
            }
            response.on("end", function() {
                callback(body)
            });
            response.on("data", function(chunk) {
                if (response.statusCode == 200) {
                    body += chunk
                }
            })
        })
    }
    fabric.util.loadImage = function(url, callback) {
        request(url, "binary", function(body) {
            var img = new Image();
            img.src = new Buffer(body, "binary");
            img._src = url;
            callback(img)
        })
    };
    fabric.loadSVGFromURL = function(url, callback) {
        url = url.replace(/^\n\s*/, "").replace(/\?.*$/, "").trim();
        request(url, "", function(body) {
            var doc = XML.parseFromString(body);
            fabric.parseSVGDocument(doc.documentElement, function(results, options) {
                callback(results, options)
            })
        })
    };
    fabric.util.getScript = function(url, callback) {
        request(url, "", function(body) {
            eval(body);
            callback && callback()
        })
    };
    fabric.Image.fromObject = function(object, callback) {
        fabric.util.loadImage(object.src, function(img) {
            var oImg = new fabric.Image(img);
            oImg._initConfig(object);
            oImg._initFilters(object);
            callback(oImg)
        })
    };
    fabric.createCanvasForNode = function(width, height) {
        var canvasEl = fabric.document.createElement("canvas"), nodeCanvas = new Canvas(width || 600, height || 600);
        canvasEl.style = {};
        canvasEl.width = nodeCanvas.width;
        canvasEl.height = nodeCanvas.height;
        var canvas = fabric.Canvas || fabric.StaticCanvas;
        var fabricCanvas = new canvas(canvasEl);
        fabricCanvas.contextContainer = nodeCanvas.getContext("2d");
        fabricCanvas.nodeCanvas = nodeCanvas;
        return fabricCanvas
    };
    fabric.StaticCanvas.prototype.createPNGStream = function() {
        return this.nodeCanvas.createPNGStream()
    };
    if (fabric.Canvas) {
        fabric.Canvas.prototype.createPNGStream
    }
    var origSetWidth = fabric.StaticCanvas.prototype.setWidth;
    fabric.StaticCanvas.prototype.setWidth = function(width) {
        origSetWidth.call(this);
        this.nodeCanvas.width = width;
        return this
    };
    if (fabric.Canvas) {
        fabric.Canvas.prototype.setWidth = fabric.StaticCanvas.prototype.setWidth
    }
    var origSetHeight = fabric.StaticCanvas.prototype.setHeight;
    fabric.StaticCanvas.prototype.setHeight = function(height) {
        origSetHeight.call(this);
        this.nodeCanvas.height = height;
        return this
    };
    if (fabric.Canvas) {
        fabric.Canvas.prototype.setHeight = fabric.StaticCanvas.prototype.setHeight
    }
})();
(function() {
    if (!this.fabric) {
        this.fabric = {}
    }
    if (fabric.AddCommand) {
        return
    }
    fabric.AddCommand = Class.create({initialize: function(b, a) {
            this.receiver = b;
            this.controller = a
        },execute: function() {
            this.controller.addObject(this.receiver, true)
        },undo: function() {
            this.controller.removeObject(this.receiver, true)
        }})
})();
(function() {
    if (!this.fabric) {
        this.fabric = {}
    }
    if (fabric.RemoveCommand) {
        return
    }
    fabric.RemoveCommand = Class.create({initialize: function(b, a) {
            this.receiver = b;
            this.controller = a
        },execute: function() {
            this.controller.removeObject(this.receiver, true)
        },undo: function() {
            this.controller.addObject(this.receiver, true)
        }})
})();
(function() {
    if (!this.fabric) {
        this.fabric = {}
    }
    if (fabric.TransformCommand) {
        return
    }
    fabric.TransformCommand = Class.create({onBeforeUndo: Prototype.emptyFunction,onAfterUndo: Prototype.emptyFunction,initialize: function(b, a) {
            a = a || {};
            this._initCallbacks(a);
            this.receiver = b;
            this._initStateProperties(a);
            this.state = {};
            this._saveState();
            this.originalState = {};
            this._saveOriginalState()
        },execute: function() {
            this._restoreState();
            this.receiver.setCoords()
        },undo: function() {
            this.onBeforeUndo(this.receiver);
            this._restoreOriginalState();
            this.receiver.setCoords();
            this.onAfterUndo(this.receiver)
        },_initStateProperties: function(a) {
            this.stateProperties = this.receiver.stateProperties;
            if (a.stateProperties && a.stateProperties.length) {
                this.stateProperties.push.apply(this.stateProperties, a.stateProperties)
            }
        },_restoreState: function() {
            this.stateProperties.each(function(a) {
                this.receiver.set(a, this.state[a])
            }, this)
        },_restoreOriginalState: function() {
            this.stateProperties.each(function(a) {
                this.receiver.set(a, this.originalState[a])
            }, this)
        },_initCallbacks: function(a) {
            if (typeof a.onBeforeUndo == "function") {
                this.onBeforeUndo = a.onBeforeUndo
            }
            if (typeof a.onAfterUndo == "function") {
                this.onAfterUndo = a.onAfterUndo
            }
        },_saveState: function() {
            this.stateProperties.each(function(a) {
                this.state[a] = this.receiver.get(a)
            }, this)
        },_saveOriginalState: function() {
            this.stateProperties.each(function(a) {
                this.originalState[a] = this.receiver.originalState[a]
            }, this)
        }})
})();
(function() {
    if (!this.fabric) {
        this.fabric = {}
    }
    if (fabric.GroupAddCommand) {
        return
    }
    fabric.GroupAddCommand = Class.create({initialize: function(b, a) {
            this.receiver = b;
            this.controller = a
        },execute: function() {
            this.controller.addGroup(this.receiver, true)
        },undo: function() {
            this.controller.canvas.discardActiveGroup()
        }})
})();
(function() {
    if (!this.fabric) {
        this.fabric = {}
    }
    if (fabric.GroupRemoveCommand) {
        return
    }
    fabric.GroupRemoveCommand = Class.create({initialize: function(b, a) {
            this.receiver = b;
            this.controller = a
        },execute: function() {
            this.controller.canvas.discardActiveGroup()
        },undo: function() {
            this.controller.addGroup(this.receiver, true)
        }})
})();
(function() {
    if (!this.fabric) {
        this.fabric = {}
    }
    if (fabric.CommandHistory) {
        return
    }
    fabric.CommandHistory = Class.create({initialize: function(a) {
            a = a || {};
            this.commands = [];
            this.index = 0;
            this.backTrigger = a.backTrigger;
            this.forwardTrigger = a.forwardTrigger;
            this._refreshTriggers()
        },getIndex: function() {
            return this.index
        },back: function() {
            if (this.index > 0) {
                this.commands[--this.index].undo();
                this._refreshTriggers();
                document.fire(fabric.CommandHistory.CHANGE_EVENT)
            }
            return this
        },forward: function() {
            if (this.index < this.commands.length) {
                this.commands[this.index++].execute();
                this._refreshTriggers();
                document.fire(fabric.CommandHistory.CHANGE_EVENT)
            }
            return this
        },add: function(a) {
            if (this.commands.length) {
                this.commands.splice(this.index, this.commands.length - this.index)
            }
            this.commands.push(a);
            this.index++;
            this._refreshTriggers();
            document.fire(fabric.CommandHistory.CHANGE_EVENT);
            return this
        },clear: function() {
            this.commands.clear();
            this.index = 0;
            this._refreshTriggers();
            return this
        },_disableTrigger: function(a) {
            Field.disable(a).addClassName("disabled")
        },_enableTrigger: function(a) {
            Field.enable(a).removeClassName("disabled")
        },_refreshTriggers: function() {
            if (this.index < 1) {
                this._disableTrigger(this.backTrigger)
            } else {
                this._enableTrigger(this.backTrigger)
            }
            if (this.index === this.commands.length) {
                this._disableTrigger(this.forwardTrigger)
            } else {
                this._enableTrigger(this.forwardTrigger)
            }
        }});
    fabric.CommandHistory.CHANGE_EVENT = "history:changed"
})();
var __fontDefinitions = {Modernist_One_400: 85,Quake_Cyr: 100,Terminator_Cyr: 10,Vampire95: 85,Encient_German_Gothic_400: 110,OdessaScript_500: 180,Globus_500: 100,CrashCTT_400: 60,CA_BND_Web_Bold_700: 60,Delicious_500: 50,Tallys_400: 70,DejaVu_Serif_400: 100,Capitalist: 70,Andale_Mono: 0,Cochin: 130,Comic_Sans_MS: 180,Courier_New: 0,Geneva: 100,Verdana: 100,Monaco: 0,Gothic: 140,Hoefler_Text: 180,Impact: 120,Lest: 100,Marker_Felt: 150,Lucida_Grande: 120,Helvetica: 120,Myriad_Pro: 120,Georgia: 120,Times_New_Roman: 120};
if (typeof global !== "undefined") {
    global.__fontDefinitions = __fontDefinitions
}
;
var Position = {includeScrollOffsets: false,prepare: function() {
        this.deltaX = window.pageXOffset || document.documentElement.scrollLeft || document.body.scrollLeft || 0;
        this.deltaY = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0
    },within: function(b, a, c) {
        if (this.includeScrollOffsets) {
            return this.withinIncludingScrolloffsets(b, a, c)
        }
        this.xcomp = a;
        this.ycomp = c;
        this.offset = Element.cumulativeOffset(b);
        return (c >= this.offset[1] && c < this.offset[1] + b.offsetHeight && a >= this.offset[0] && a < this.offset[0] + b.offsetWidth)
    },withinIncludingScrolloffsets: function(b, a, d) {
        var c = Element.cumulativeScrollOffset(b);
        this.xcomp = a + c[0] - this.deltaX;
        this.ycomp = d + c[1] - this.deltaY;
        this.offset = Element.cumulativeOffset(b);
        return (this.ycomp >= this.offset[1] && this.ycomp < this.offset[1] + b.offsetHeight && this.xcomp >= this.offset[0] && this.xcomp < this.offset[0] + b.offsetWidth)
    },overlap: function(b, a) {
        if (!b) {
            return 0
        }
        if (b == "vertical") {
            return ((this.offset[1] + a.offsetHeight) - this.ycomp) / a.offsetHeight
        }
        if (b == "horizontal") {
            return ((this.offset[0] + a.offsetWidth) - this.xcomp) / a.offsetWidth
        }
    },cumulativeOffset: Element.Methods.cumulativeOffset,positionedOffset: Element.Methods.positionedOffset,absolutize: function(a) {
        Position.prepare();
        return Element.absolutize(a)
    },relativize: function(a) {
        Position.prepare();
        return Element.relativize(a)
    },realOffset: Element.Methods.cumulativeScrollOffset,offsetParent: Element.Methods.getOffsetParent,page: Element.Methods.viewportOffset,clone: function(b, c, a) {
        a = a || {};
        return Element.clonePosition(c, b, a)
    }};
if (!Control) {
    var Control = {}
}
Control.Slider = Class.create({initialize: function(d, a, b) {
        var c = this;
        if (Object.isArray(d)) {
            this.handles = d.collect(function(f) {
                return $(f)
            })
        } else {
            this.handles = [$(d)]
        }
        this.track = $(a);
        this.options = b || {};
        this.axis = this.options.axis || "horizontal";
        this.increment = this.options.increment || 1;
        this.step = parseInt(this.options.step || "1");
        this.range = this.options.range || $R(0, 1);
        this.value = 0;
        this.values = this.handles.map(function() {
            return 0
        });
        this.spans = this.options.spans ? this.options.spans.map(function(e) {
            return $(e)
        }) : false;
        this.options.startSpan = $(this.options.startSpan || null);
        this.options.endSpan = $(this.options.endSpan || null);
        this.restricted = this.options.restricted || false;
        this.maximum = this.options.maximum || this.range.end;
        this.minimum = this.options.minimum || this.range.start;
        this.alignX = parseInt(this.options.alignX || "0");
        this.alignY = parseInt(this.options.alignY || "0");
        this.trackLength = this.maximumOffset() - this.minimumOffset();
        this.handleLength = this.isVertical() ? (this.handles[0].offsetHeight != 0 ? this.handles[0].offsetHeight : this.handles[0].style.height.replace(/px$/, "")) : (this.handles[0].offsetWidth != 0 ? this.handles[0].offsetWidth : this.handles[0].style.width.replace(/px$/, ""));
        this.active = false;
        this.dragging = false;
        this.disabled = false;
        if (this.options.disabled) {
            this.setDisabled()
        }
        this.allowedValues = this.options.values ? this.options.values.sortBy(Prototype.K) : false;
        if (this.allowedValues) {
            this.minimum = this.allowedValues.min();
            this.maximum = this.allowedValues.max()
        }
        this.eventMouseDown = this.startDrag.bind(this);
        this.eventMouseUp = this.endDrag.bind(this);
        this.eventMouseMove = this.update.bind(this);
        this.handles.each(function(f, e) {
            e = c.handles.length - 1 - e;
            c.setValue(parseFloat((Object.isArray(c.options.sliderValue) ? c.options.sliderValue[e] : c.options.sliderValue) || c.range.start), e);
            f.makePositioned().observe("mousedown", c.eventMouseDown)
        });
        this.track.observe("mousedown", this.eventMouseDown);
        document.observe("mouseup", this.eventMouseUp);
        document.observe("mousemove", this.eventMouseMove);
        this.initialized = true
    },dispose: function() {
        var a = this;
        Event.stopObserving(this.track, "mousedown", this.eventMouseDown);
        Event.stopObserving(document, "mouseup", this.eventMouseUp);
        Event.stopObserving(document, "mousemove", this.eventMouseMove);
        this.handles.each(function(b) {
            Event.stopObserving(b, "mousedown", a.eventMouseDown)
        })
    },setDisabled: function() {
        this.disabled = true
    },setEnabled: function() {
        this.disabled = false
    },getNearestValue: function(a) {
        if (this.allowedValues) {
            if (a >= this.allowedValues.max()) {
                return (this.allowedValues.max())
            }
            if (a <= this.allowedValues.min()) {
                return (this.allowedValues.min())
            }
            var c = Math.abs(this.allowedValues[0] - a), b = this.allowedValues[0];
            this.allowedValues.each(function(d) {
                var e = Math.abs(d - a);
                if (e <= c) {
                    b = d;
                    c = e
                }
            });
            return b
        }
        if (a > this.range.end) {
            return this.range.end
        }
        if (a < this.range.start) {
            return this.range.start
        }
        return a
    },setValue: function(c, b, a) {
        if (!this.active) {
            this.activeHandleIdx = b || 0;
            this.activeHandle = this.handles[this.activeHandleIdx];
            this.updateStyles()
        }
        b = b || this.activeHandleIdx || 0;
        if (this.initialized && this.restricted) {
            if ((b > 0) && (c < this.values[b - 1])) {
                c = this.values[b - 1]
            }
            if ((b < (this.handles.length - 1)) && (c > this.values[b + 1])) {
                c = this.values[b + 1]
            }
        }
        c = this.getNearestValue(c);
        this.values[b] = c;
        this.value = this.values[0];
        this.handles[b].style[this.isVertical() ? "top" : "left"] = this.translateToPx(c);
        this.drawSpans();
        if (!this.dragging || !this.event) {
            this.updateFinished(a)
        }
    },setValueBy: function(b, a) {
        this.setValue(this.values[a || this.activeHandleIdx || 0] + b, a || this.activeHandleIdx || 0)
    },translateToPx: function(a) {
        return Math.round(((this.trackLength - this.handleLength) / (this.range.end - this.range.start)) * (a - this.range.start)) + "px"
    },translateToValue: function(a) {
        return ((a / (this.trackLength - this.handleLength) * (this.range.end - this.range.start)) + this.range.start)
    },getRange: function(b) {
        var a = this.values.sortBy(Prototype.K);
        b = b || 0;
        return $R(a[b], a[b + 1])
    },minimumOffset: function() {
        return (this.isVertical() ? this.alignY : this.alignX)
    },maximumOffset: function() {
        return (this.isVertical() ? (this.track.offsetHeight != 0 ? this.track.offsetHeight : this.track.style.height.replace(/px$/, "")) - this.alignY : (this.track.offsetWidth != 0 ? this.track.offsetWidth : this.track.style.width.replace(/px$/, "")) - this.alignX)
    },isVertical: function() {
        return (this.axis == "vertical")
    },drawSpans: function() {
        var a = this;
        if (this.spans) {
            $R(0, this.spans.length - 1).each(function(b) {
                a.setSpan(a.spans[b], a.getRange(b))
            })
        }
        if (this.options.startSpan) {
            this.setSpan(this.options.startSpan, $R(0, this.values.length > 1 ? this.getRange(0).min() : this.value))
        }
        if (this.options.endSpan) {
            this.setSpan(this.options.endSpan, $R(this.values.length > 1 ? this.getRange(this.spans.length - 1).max() : this.value, this.maximum))
        }
    },setSpan: function(b, a) {
        if (this.isVertical()) {
            b.style.top = this.translateToPx(a.start);
            b.style.height = this.translateToPx(a.end - a.start + this.range.start)
        } else {
            b.style.left = this.translateToPx(a.start);
            b.style.width = this.translateToPx(a.end - a.start + this.range.start)
        }
    },updateStyles: function() {
        this.handles.each(function(a) {
            Element.removeClassName(a, "selected")
        });
        Element.addClassName(this.activeHandle, "selected")
    },startDrag: function(c) {
        if (Event.isLeftClick(c)) {
            if (!this.disabled) {
                this.active = true;
                if (this.options.onStart) {
                    this.options.onStart(this.values.length > 1 ? this.values : this.value, this)
                }
                var d = Event.element(c), e = Event.pointer(c);
                e = [e.x, e.y];
                var a = d;
                if (a == this.track) {
                    var b = Position.cumulativeOffset(this.track);
                    this.event = c;
                    this.setValue(this.translateToValue((this.isVertical() ? e[1] - b[1] : e[0] - b[0]) - (this.handleLength / 2)));
                    b = Position.cumulativeOffset(this.activeHandle);
                    this.offsetX = (e[0] - b[0]);
                    this.offsetY = (e[1] - b[1])
                } else {
                    while ((this.handles.indexOf(d) == -1) && d.parentNode) {
                        d = d.parentNode
                    }
                    if (this.handles.indexOf(d) != -1) {
                        this.activeHandle = d;
                        this.activeHandleIdx = this.handles.indexOf(this.activeHandle);
                        this.updateStyles();
                        var b = Position.cumulativeOffset(this.activeHandle);
                        this.offsetX = (e[0] - b[0]);
                        this.offsetY = (e[1] - b[1])
                    }
                }
            }
            Event.stop(c)
        }
    },update: function(a) {
        if (this.active) {
            if (!this.dragging) {
                this.dragging = true
            }
            this.draw(a);
            if (Prototype.Browser.WebKit) {
                window.scrollBy(0, 0)
            }
            Event.stop(a)
        }
    },draw: function(b) {
        var c = Event.pointer(b), a = Position.cumulativeOffset(this.track);
        c.x -= this.offsetX + a[0];
        c.y -= this.offsetY + a[1];
        this.event = b;
        this.setValue(this.translateToValue(this.isVertical() ? c.y : c.x));
        if (this.initialized && this.options.onSlide) {
            this.options.onSlide(this.values.length > 1 ? this.values : this.value, this)
        }
    },endDrag: function(a) {
        if (this.active && this.dragging) {
            this.finishDrag(a, true);
            Event.stop(a)
        }
        this.active = false;
        this.dragging = false
    },finishDrag: function(a, b) {
        this.active = false;
        this.dragging = false;
        this.updateFinished()
    },updateFinished: function(a) {
        if (this.initialized && this.options.onChange && !a) {
            this.options.onChange(this.values.length > 1 ? this.values : this.value, this)
        }
        this.event = null
    }});
APE.namespace("APE.dom");
(function() {
    var b = APE.dom;
    b.getPixelCoords = a;
    function a(c) {
        var d = (b.IS_COMPUTED_STYLE ? function(f) {
            var e = f.ownerDocument.defaultView.getComputedStyle(f, "");
            return {x: parseInt(e.left) || 0,y: parseInt(e.top) || 0}
        } : function(f) {
            var e = f.style;
            return {x: e.pixelLeft || parseInt(b.getStyle(f, "left")) || 0,y: e.pixelTop || parseInt(b.getStyle(f, "top")) || 0}
        });
        this.getPixelCoords = d;
        return d(c)
    }
})();
APE.namespace("APE.dom");
(function() {
    var c = APE.dom, a = c.getStyle;
    c.getContainingBlock = b;
    function b(e) {
        var g = a(e, "position"), f = e.ownerDocument.documentElement, d = e.parentNode;
        if (/^(?:r|s)/.test(g) || !g) {
            return d
        }
        if (g == "fixed") {
            return null
        }
        while (d && d != f && (typeof d.nodeType == "number" && d.nodeType !== 11)) {
            if (b(d, "position") != "static") {
                return d
            }
            d = d.parentNode
        }
        return f
    }
})();
APE.namespace("APE.drag");
APE.drag.Draggable = function(a, b) {
    this.id = a.id;
    this.el = this.origEl = a;
    this.style = a.style;
    this.isRel = APE.dom.getStyle(a, "position").toLowerCase() == "relative";
    this.container = (this.isRel ? a.parentNode : APE.dom.getContainingBlock(a));
    this.dropTargets = [];
    this.handle = a;
    this.constraint = b || 0;
    this.init()
};
APE.drag.Draggable.getByNode = APE.getByNode;
APE.drag.Draggable.instanceDestructor = function() {
    var b, c, d, a = APE.drag.DragHandlers;
    for (b in this.instances) {
        d = this.instances[b];
        for (c in d) {
            if (d.hasOwnProperty(c)) {
                delete d[c]
            }
        }
        delete this.instances[b]
    }
    if (d) {
        d.constructor.draggableList = {};
        a.focusedDO = a.dO = null
    }
};
APE.drag.Draggable.focused = function(b) {
    if (a - arguments.callee.timeStamp < 5) {
        return
    }
    arguments.callee.timeStamp = a;
    b = b || event;
    var a = new Date - 0;
    if (typeof b.stopPropagation == "function") {
        b.stopPropagation()
    } else {
        b.cancelBubble = true
    }
    this.setFocus(true, b)
};
APE.drag.Draggable.blurred = function(a) {
    this.setFocus(false, a)
};
APE.drag.Draggable.constraints = {NONE: 0,HORZ: 1,VERT: 2};
APE.drag.Draggable.prototype = {hasFocus: false,dragCopy: false,dragMultiple: false,isSelected: false,_dragOverTargets: false,onfocus: undefined,onblur: undefined,onbeforedrag: undefined,onbeforedragstart: undefined,ondragstart: undefined,ondrag: undefined,ondragstop: undefined,ondragend: undefined,x: 0,y: 0,origX: 0,origY: 0,grabX: 0,grabY: 0,newX: 0,newY: 0,constraint: APE.drag.Draggable.constraints.NONE,keepInContainer: false,isDragEnabled: true,selectedClassName: "",activeDragClassName: "",focusClassName: "",init: function() {
        var d = APE.EventPublisher, c = APE.drag, a = c.Draggable, b = this.el;
        b.style.zIndex = APE.dom.getStyle(b, "zIndex") || a.highestZIndex++;
        this._setIeTopLeft();
        d.add(b, "onfocus", a.focused, this);
        d.add(b, "onblur", a.blurred, this);
        if (!b.getAttribute("tabIndex")) {
            b.tabIndex = 0
        }
        this.onbeforeexitcontainer = function() {
            return !this.keepInContainer
        };
        c.DragHandlers.init()
    },useHandleTree: true,hasHandleSet: false,setHandle: function(a, b) {
        this.handle = a;
        this.hasHandleSet = true;
        this.useHandleTree = b != false
    },isInHandle: function(a) {
        return a == this.handle || (this.useHandleTree && APE.dom.contains(this.handle, a))
    },addDropTarget: function(d) {
        var b = APE.drag.DropTarget, a = b.getByNode(d).el, c = this.dropTargets;
        if (this.el === a) {
            return
        }
        return c[c.length] = b.getByNode(a)
    },grab: function(k, m, d) {
        if (!k) {
            k = event
        }
        var g = APE.dom, p = g.Event, l = p.getTarget(k), j = APE.drag, o = j.DragHandlers;
        if (k.preventDefault) {
            k.preventDefault()
        }
        k.returnValue = false;
        if (g.contains(this.el, l)) {
            return
        }
        this._fixFocus(k);
        var c = g.getPixelCoords(this.el);
        this.grabX = c.x;
        this.grabY = c.y;
        var n = p.getCoords(k), b = g.getOffsetCoords(g.getContainingBlock(this.el)), h = n.y - b.y, r = Math.floor(h - (this.handle.offsetHeight / 2)), q = g.getOffsetCoords(this.handle, this.el), f = j.Draggable.constraints;
        if (this.constraint != f.VERT) {
            var i = n.x - b.x, a = i - Math.floor((this.handle.offsetWidth / 2));
            if (this.keepInContainer) {
                a = Math.max(a, 0);
                a = Math.min(a, this.container.clientWidth - this.el.offsetWidth)
            }
            this.moveToX(a - q.x + (m || 0))
        }
        if (this.constraint != f.HORZ) {
            if (this.keepInContainer) {
                r = Math.max(r, 0);
                r = Math.min(r, this.container.clientHeight - this.el.offsetHeight)
            }
            this.moveToY(r - q.y + (d || 0))
        }
        o.dragObjGrabbed(k, this);
        o.dO = this
    },select: function(c) {
        var b = window.APE, a = b.drag.Draggable;
        if (c) {
            if (this.selectedClassName) {
                b.dom.addClass(this.el, this.selectedClassName)
            }
            if (this.dragMultiple && !(this.id in a.draggableList)) {
                a.draggableList[this.id] = this
            }
        } else {
            if (this.selectedClassName) {
                b.dom.removeClass(this.el, this.selectedClassName)
            }
            delete a.draggableList[this.id]
        }
        this.isSelected = Boolean(c)
    },setFocus: function(c, d) {
        if (c == this.hasFocus) {
            return
        }
        if (!this.isDragEnabled) {
            return false
        }
        var b = true, a = APE.drag.DragHandlers, f = APE.dom;
        if (c) {
            if (this.focusClassName) {
                f.addClass(this.el, this.focusClassName)
            }
            if (typeof this.onfocus == "function") {
                b = this.onfocus(d)
            }
            if (b != false) {
                if (a.focusedDO) {
                    a.focusedDO.setFocus(false, d)
                }
                a.focusedDO = this
            }
        } else {
            if (this.focusClassName) {
                f.removeClass(this.el, this.focusClassName)
            }
            if (typeof this.onblur == "function") {
                b = this.onblur(d)
            }
            if (b != false) {
                a.focusedDO = null
            }
        }
        this.hasFocus = c;
        return b
    },_dragOverTargets: false,_fixFocus: function(a) {
        var b = a.metaKey || (/Win/.test(navigator.platform) && a.ctrlKey);
        if (!this.dragMultiple && APE.drag.DragHandlers.focusedDO && b) {
            return false
        }
        if (!this.hasFocus) {
            this.setFocus(!this.hasFocus, a)
        }
    },dragStart: function(a) {
        if (this.isBeingDragged) {
            return
        }
        if (this.dragCopy) {
            this.assignClone(a)
        }
        if (typeof this.ondragstart == "function") {
            this.ondragstart(a)
        }
        if (this.activeDragClassName) {
            APE.dom.addClass(this.el, this.activeDragClassName)
        }
        APE.drag.DragHandlers.setUpCoords(a, this);
        this.isBeingDragged = true
    },release: function(a) {
        APE.drag.DragHandlers.dragObjReleased(a, this);
        if (typeof this.onrelease == "function") {
            this.onrelease(a)
        }
    },assignClone: function(d) {
        var h = APE.dom, c = "addClass", g, b = this.el, f = b, a;
        if (!this.copyEl) {
            this.origEl = b;
            this.copyEl = b.cloneNode(true)
        }
        g = this.copyEl;
        a = g.style;
        if (this.focusClassName) {
            h[this.hasFocus ? c : "removeClass"](g, this.focusClassName)
        }
        a.display = "";
        if (g.parentNode != b.parentNode) {
            b.parentNode.insertBefore(g, b)
        }
        a.zIndex = parseInt(f.style.zIndex) + 100;
        if (this.origClassName) {
            h[c](b, this.origClassName)
        }
        this.el = g;
        this.style = a;
        if (this.isRel) {
            a.marginBottom = -f.offsetHeight + -(parseInt(h.getStyle(f, "marginBottom")) || 0) + "px";
            a.marginright = -f.offsetWidth + -(parseInt(h.getStyle(f, "marginRight")) || 0) + "px"
        }
    },retireClone: function() {
        this.constructor.focused.timeStamp = new Date;
        if (this.copyEl.style.display == "none") {
            return
        }
        this.el = this.origEl;
        this.style = this.origEl.style;
        this.moveToX(this.x);
        this.moveToY(this.y);
        this.copyEl.style.display = "none";
        if (this.origClassName) {
            APE.dom.removeClass(this.el, this.origClassName)
        }
    },moveToX: ("pixelLeft" in document.documentElement.style ? function(a) {
        this.style.pixelLeft = this.x = a
    } : function(a) {
        this.style.left = (this.x = a) + "px"
    }),moveToY: ("pixelTop" in document.documentElement.style ? function(a) {
        this.style.pixelTop = this.y = a
    } : function(a) {
        this.style.top = (this.y = a) + "px"
    }),moveToXY: ("pixelTop" in document.documentElement.style ? function(a, c) {
        var b = this.style;
        b.pixelLeft = this.x = a;
        b.pixelTop = this.y = c
    } : function(a, c) {
        var b = this.style;
        b.left = (this.x = a) + "px";
        b.top = (this.y = c) + "px"
    }),glideStart: function(a, d) {
        if (this.animTimer) {
            return
        }
        this.startX = a;
        this.startY = d;
        var c = this.startX - this.grabX, b = this.startY - this.grabY;
        this.GlideDist = Math.ceil(Math.sqrt((c * c) + (b * b)));
        if (this.GlideDist === 0) {
            return
        }
        this.rx = Math.abs(c) / this.GlideDist;
        this.ry = Math.abs(b) / this.GlideDist;
        if (this.x > this.grabX) {
            this.rx = -this.rx
        }
        if (this.y > this.grabY) {
            this.ry = -this.ry
        }
        this.startTime = new Date().getTime();
        this.animTimer = window.setInterval("APE.drag.Draggable.instances['" + this.id + "'].glide()", 10)
    },glide: function() {
        var a = new Date - this.startTime, b = Math.ceil(2 * a + 0.5 * 0.01 * a * a);
        if (b >= this.GlideDist) {
            this.animTimer = clearInterval(this.animTimer);
            if (this.constraint != 2) {
                this.moveToX(this.grabX)
            }
            if (this.constraint != 1) {
                this.moveToY(this.grabY)
            }
            if (this.copyEl) {
                this.el = this.origEl;
                this.style = this.origEl.style;
                this.copyEl.style.display = "none"
            }
            if (typeof this.onglide == "function") {
                this.onglide()
            }
            if (typeof this.onglideend == "function") {
                this.onglideend()
            }
            this.dragDone({})
        } else {
            if (this.constraint != 2) {
                this.moveToX(this.startX + b * this.rx)
            }
            if (this.constraint != 1) {
                this.moveToY(this.startY + b * this.ry)
            }
            if (typeof this.onglide == "function") {
                this.onglide()
            }
        }
    },animateBack: function(a, b) {
        this.glideStart(a || this.x, b || this.y)
    },setContainer: function(a) {
        this.container = a
    },removeDropTarget: function(c) {
        c = document.getElementById(c.id);
        for (var b = 0, a = this.dropTargets.length; b < a; b++) {
            if (this.dropTargets[b].el === c) {
                this.dropTargets.splice(b, 1);
                return c
            }
        }
        return null
    },dragDone: function(a) {
        if (this.activeDragClassName) {
            APE.dom.removeClass(this.el, this.activeDragClassName)
        }
        if (typeof this.ondragend == "function" && this.hasBeenDragged) {
            this.ondragend(a)
        }
        if (this.copyEl) {
            this.el.parentNode.insertBefore(this.copyEl, this.el)
        }
        this.hasBeenDragged = false
    },_setIeTopLeft: function() {
        var b = document.defaultView, c = this.el, i = c.style, g = c.currentStyle || (b.getComputedStyle && b.getComputedStyle(c, "")) || i, e = APE.dom.getContainingBlock(c), h = g.left, f = g.right, d = g.top, a = g.bottom;
        if ((h == "" || h == "auto")) {
            f = parseInt(f);
            if (isFinite(f)) {
                i.left = e.clientWidth - c.offsetWidth - f + "px"
            } else {
                i.left = "0"
            }
        }
        if ((d == "" || d == "auto")) {
            a = parseInt(a);
            if (isFinite(a)) {
                i.top = e.clientHeight - c.offsetHeight - a + "px"
            } else {
                i.top = "0"
            }
        }
    },toString: function() {
        return "APE.drag.Draggable(id=" + this.id + ")"
    }};
APE.drag.Draggable.highestZIndex = 1000;
APE.drag.Draggable.draggableList = {};
APE.drag.Draggable._setUpDragOver = function(e) {
    e._dragOverTargets = [];
    var d = e.dropTargets, c, b = 0, a = d.length;
    for (; b < a; b++) {
        c = d[b];
        c.initCoords();
        if (typeof c.ondragover == "function" || typeof c.ondragout == "function" || c.dragOverClassName) {
            e._dragOverTargets.push(c)
        }
    }
    if (e._dragOverTargets.length === 0) {
        e._dragOverTargets = false
    }
};
APE.drag.DropTarget = function(a) {
    this.el = a;
    this.id = a.id
};
APE.drag.DropTarget.getByNode = APE.getByNode;
APE.drag.DropTarget.prototype = {coords: undefined,dragOverClassName: "",initCoords: function() {
        if (!this.coords) {
            this.coords = {}
        }
        APE.dom.getOffsetCoords(this.el, document, this.coords);
        this.coords.w = this.el.clientWidth;
        this.coords.h = this.el.clientHeight
    },containsCoords: function(a) {
        var c = this.coords.x, b = this.coords.y;
        return (a.x >= c && a.x <= c + this.coords.w) && (a.y >= b && a.y <= b + this.coords.h)
    },ondragover: false,ondragout: undefined,ondrop: undefined};
APE.drag.DragHandlers = {dO: null,focusedDO: null,getEventCoords: APE.dom.Event.getCoords,init: function() {
        if (this.inited) {
            return
        }
        var c = document, e = c.documentElement, b = e.style, a = APE.EventPublisher;
        a.add(c, "onmousedown", this.mouseDown, this);
        a.add(c, "onkeypress", this.keyPressed, this);
        a.add(c, "onmousemove", this.mouseMove, this);
        a.add(c, "onmouseup", this.mouseUp, this);
        if ("onselectstart" in c) {
            a.get(c, "onselectstart").addBefore(this.isInDrag, this)
        } else {
            a.get(c, "onmousedown").addAfter(this.preventUserSelection, this);
            a.get(c, "onmouseup").addAfter(this.preventUserSelection, this)
        }
        this.inited = true;
        this.userSelectType = "MozUserSelect" in b ? "MozUserSelect" : "KhtmlUserSelect" in b ? "KhtmlUserSelect" : "userSelect" in b ? "userSelect" : ""
    },isInDrag: function() {
        return !this.dO && !this.focusedDO
    },preventUserSelection: function() {
        document.documentElement.style[this.userSelectType] = this.dO ? "none" : ""
    },dragObjGrabbed: function(d, g) {
        if (typeof g.onbeforedragstart == "function" && g.onbeforedragstart(d) == false) {
            return true
        }
        var a = APE.drag.DragHandlers, f = APE.dom, c = f.Event.getCoords(d), b;
        a.locked = true;
        a.mousedownX = c.x;
        a.mousedownY = c.y;
        b = f.getPixelCoords(g.el);
        g.origX = g.grabX = b.x;
        g.origY = g.grabY = b.y;
        g.isBeingDragged = false
    },setUpCoords: function(g, j) {
        var f = APE.dom, a = j.container, b = j.el, d = f.getContainingBlock(b), k = f.getOffsetCoords(d, a), c = f.getPixelCoords(b), l = f.getOffsetCoords(b, b.parentNode), i = l.x - c.x + k.x, h = l.y - c.y + k.y;
        if (j.keepInContainer) {
            j.minX = 0 - i;
            j.maxX = a.clientWidth - j.el.offsetWidth - i;
            j.minY = 0 - h;
            j.maxY = a.clientHeight - j.el.offsetHeight - h
        }
    },mouseDown: function(f) {
        if (!f) {
            f = event
        }
        var d = APE.dom, g = d.Event.getTarget(f), i = null, c = APE.drag.Draggable, a = c.instances, j = g;
        for (; i == null && j; j = d.findAncestorWithAttribute(j, "id")) {
            i = a[j.id]
        }
        var h = f.metaKey || (/Win/.test(navigator.platform) && f.ctrlKey);
        if (i) {
            if (!i.isDragEnabled) {
                if (!h) {
                    this.removeGroupSelection();
                    if (this.focusedDO) {
                    }
                }
                return false
            }
            if (!h && i.hasHandleSet && !i.isInHandle(g)) {
                if (!this.locked) {
                    this.removeGroupSelection();
                    this.dO = null;
                    this.locked = false
                }
                return
            } else {
                if (!h && !i.isSelected) {
                    this.removeGroupSelection()
                }
                f.returnValue = false
            }
        } else {
            if (!this.locked) {
                if (!h) {
                    this.removeGroupSelection();
                    if (this.focusedDO) {
                        this.focusedDO.setFocus(false, f)
                    }
                }
                this.dO = null;
                this.locked = false
            }
            return
        }
        if (h && this.hasGroupSelection() && !i.dragMultiple) {
            return false
        }
        i._fixFocus(f);
        if (!i.dragMultiple) {
            if (!h) {
                this.removeGroupSelection()
            } else {
                return false
            }
        }
        this.setGroupSelection(i, h);
        i.style.zIndex = ++c.highestZIndex;
        if (h && i.hasFocus) {
        }
        c._setUpDragOver(i);
        this.dragObjGrabbed(f, i);
        for (var b in c.draggableList) {
            this.dragObjGrabbed(f, c.draggableList[b])
        }
        this.dO = i;
        return g.tagName != "IMG"
    },setGroupSelection: function(c, b) {
        var a = APE.drag.Draggable.draggableList;
        if (b) {
            if (c.id in a) {
                this.deselectOnMouseup = true
            } else {
                c.select(true)
            }
        } else {
            if (!c.isSelected) {
                this.removeGroupSelection();
                c.select(true)
            }
        }
    },dragObjReleased: function(g, k) {
        k.animateBack();
        var c = window.APE, l = c.dom.removeClass, h = c.drag.Draggable.draggableList, b, f = 0, d = k._dragOverTargets.length, a;
        if (k._dragOverTargets !== false) {
            for (; f < d; f++) {
                b = k._dragOverTargets[f];
                if (b.hasDropTargetOver) {
                    if (typeof b.ondragout == "function") {
                        b.ondragout(g, k)
                    }
                    if (b.dragOverClassName) {
                        l(b.el, b.dragOverClassName)
                    }
                    b.hasDropTargetOver = false
                }
            }
        }
        for (a in h) {
            h[a].animateBack()
        }
        this.dO = null
    },carryGroup: function(c, a) {
        var b = APE.drag.Draggable.draggableList, d, e;
        for (e in b) {
            d = b[e];
            if (c != null) {
                d.moveToX(d.origX + c)
            }
            if (a != null) {
                d.moveToY(d.origY + a)
            }
        }
    },removeGroupSelection: function() {
        var c, b = APE.drag, a = b.Draggable.draggableList;
        for (c in a) {
            a[c].select(false)
        }
        if (b.DragHandlers.focusedDO) {
            b.DragHandlers.focusedDO.select(false)
        }
    },hasGroupSelection: function() {
        for (var a in APE.drag.Draggable.draggableList) {
            return true
        }
        return false
    },mouseMove: function(y) {
        var f = this.dO;
        if (f == null) {
            return
        }
        if (!y) {
            y = event
        }
        var h = this.getEventCoords(y), r = h.x, p = h.y, n = r - this.mousedownX, l = p - this.mousedownY;
        if (f.isBeingDragged == false) {
            f.dragStart(y);
            var o, A = APE.drag.Draggable.draggableList;
            for (o in A) {
                A[o].dragStart(y)
            }
        }
        f.newX = f.origX + n;
        f.newY = f.origY + l;
        f.hasBeenDragged = (f.hasBeenDragged || (n || l));
        var t = APE.drag.Draggable.constraints, d = f.newX < f.minX, b = f.newX > f.maxX, B = f.newY < f.minY, q = f.newY > f.maxY;
        if (typeof f.onbeforedrag == "function" && f.onbeforedrag(y) == false) {
            return
        }
        var a = f.container != null, C = (typeof f.ondrag == "function"), c = typeof f.onbeforeexitcontainer == "function", z = 0;
        if (f.constraint === t.NONE) {
            a &= (d || b || B || q);
            if (a && (c || f.onbeforeexitcontainer() == false)) {
                if (d) {
                    if (!f.isAtLeft) {
                        f.moveToX(f.minX);
                        this.carryGroup(f.minX - f.origX, null);
                        if (C) {
                            f.ondrag(y)
                        }
                        f.isAtRight = false;
                        f.isAtLeft = true;
                        z += 1
                    }
                } else {
                    if (b) {
                        if (!f.isAtRight) {
                            f.moveToX(f.maxX);
                            this.carryGroup(f.maxX - f.origX, null);
                            if (C) {
                                f.ondrag(y)
                            }
                            f.isAtRight = true;
                            f.isAtLeft = false;
                            z += 1
                        }
                    } else {
                        f.isAtLeft = f.isAtRight = false;
                        f.moveToX(f.newX);
                        this.carryGroup(n, null)
                    }
                }
                if (B) {
                    if (!f.isAtTop) {
                        f.moveToY(f.minY);
                        this.carryGroup(null, f.minY - f.origY);
                        if (C) {
                            f.ondrag(y)
                        }
                        f.isAtTop = true;
                        f.isAtBottom = false;
                        z += 1
                    }
                } else {
                    if (q) {
                        if (!f.isAtBottom) {
                            if (f.maxY > 0) {
                                f.moveToY(f.maxY)
                            }
                            this.carryGroup(null, f.maxY - f.origY);
                            if (C) {
                                f.ondrag(y)
                            }
                            f.isAtTop = false;
                            f.isAtBottom = true;
                            z += 1
                        }
                    } else {
                        f.isAtTop = f.isAtBottom = false;
                        f.moveToY(f.newY);
                        this.carryGroup(null, l)
                    }
                }
                f.isDragStopped = z == 2;
                if (f.isDragStopped && typeof f.ondragstop == "function") {
                    f.ondragstop(y)
                } else {
                    if (C) {
                        f.ondrag(y)
                    }
                }
            } else {
                f.isDragStopped = f.isAtLeft = f.isAtRight = f.isAtTop = f.isAtBottom = false;
                f.moveToXY(f.newX, f.newY);
                this.carryGroup(n, l);
                if (C) {
                    f.ondrag(y)
                }
            }
        } else {
            if (f.constraint % 2 == 0) {
                a &= (B || q);
                if (a && (c || f.onbeforeexitcontainer() == false)) {
                    if (B) {
                        if (!f.isAtTop) {
                            f.moveToY(f.minY);
                            this.carryGroup(null, f.minY - f.origY);
                            if (C) {
                                f.ondrag(y)
                            }
                            f.isAtTop = !(f.isAtBottom = false)
                        }
                    } else {
                        if (q) {
                            if (!f.isAtBottom) {
                                f.moveToY(f.maxY);
                                this.carryGroup(null, f.maxY - f.origY);
                                if (C) {
                                    f.ondrag(y)
                                }
                                f.isAtBottom = !(f.isAtTop = false)
                            }
                        }
                    }
                    if (!f.isDragStopped) {
                        if (typeof f.ondragstop == "function") {
                            f.ondragstop(y)
                        }
                        f.isDragStopped = true
                    }
                } else {
                    f.isAtTop = f.isAtBottom = false;
                    f.isDragStopped = false;
                    f.moveToY(f.newY);
                    this.carryGroup(null, l);
                    if (C) {
                        f.ondrag(y)
                    }
                }
            } else {
                a &= (d || b);
                if (a && (c || f.onbeforeexitcontainer() == false)) {
                    if (d) {
                        if (!f.isAtLeft) {
                            f.moveToX(f.minX);
                            this.carryGroup(f.minX - f.origX, null);
                            f.isAtLeft = !(f.isAtRight = false)
                        }
                    } else {
                        if (b) {
                            if (!f.isAtRight) {
                                this.carryGroup(f.maxX - f.origX, null);
                                f.moveToX(f.maxX);
                                f.isAtRight = !(f.isAtLeft = false)
                            }
                        }
                    }
                    if (!f.isDragStopped) {
                        if (typeof f.ondragstop == "function") {
                            f.ondragstop(y)
                        }
                        f.isDragStopped = true
                    }
                } else {
                    f.isAtLeft = f.isAtRight = false;
                    f.isDragStopped = false;
                    f.moveToX(f.newX);
                    this.carryGroup(n, null);
                    if (C) {
                        f.ondrag(y)
                    }
                }
            }
        }
        var s = f._dragOverTargets;
        if (s !== false) {
            var w = {x: r,y: p}, v = 0, u = s.length, m, x = APE.dom, k, g = {domEvent: y,dragObj: f};
            for (; v < u; v++) {
                m = s[v], k = m.containsCoords(w);
                if (!m.hasDropTargetOver && k) {
                    m.hasDropTargetOver = true;
                    if (typeof m.ondragover == "function") {
                        m.ondragover(g)
                    }
                    if (m.dragOverClassName) {
                        x.addClass(m.el, m.dragOverClassName)
                    }
                } else {
                    if (m.hasDropTargetOver && !k) {
                        if (typeof m.ondragout == "function") {
                            m.ondragout(g)
                        }
                        if (m.dragOverClassName) {
                            x.removeClass(m.el, m.dragOverClassName)
                        }
                        m.hasDropTargetOver = false
                    }
                }
            }
        }
        return false
    },mouseUp: function(j) {
        var d = (this.dO && this.dO.isBeingDragged && !this.dO.hasBeenDragged);
        if (this.dO == null || !this.dO.hasBeenDragged && !d) {
            if (this.dO && this.deselectOnMouseup && !this.dO.hasBeenDragged) {
                this.dO.select(false)
            }
            this.deselectOnMouseup = false;
            this.dO = null;
            this.locked = false;
            return
        }
        if (!j) {
            j = event
        }
        var p = this.dO, k = APE.drag.Draggable.draggableList, b, q;
        if (p.copyEl) {
            p.retireClone()
        }
        for (b in k) {
            q = k[b];
            if (q.copyEl) {
                q.retireClone()
            }
        }
        var g = p.dropTargets, h = g.length, c, n, l;
        if (h > 0) {
            var m = this.getEventCoords(j), a, f = 0;
            for (; f < h; f++) {
                a = g[f];
                if (a.containsCoords(m)) {
                    a.containsCoords(m);
                    if (typeof a.ondrop == "function") {
                        a.ondrop({domEvent: j,dragObj: p,dropTarget: a})
                    }
                    for (b in k) {
                        if (b === a.id) {
                            continue
                        }
                        if (typeof a.ondrop == "function") {
                            c = k[b];
                            a.ondrop({domEvent: j,dragObj: c,dropTarget: a})
                        }
                    }
                    if (a.dragOverClassName) {
                        APE.dom.removeClass(a.el, a.dragOverClassName)
                    }
                    break
                }
            }
        }
        for (b in k) {
            c = k[b], n = c.x, l = c.y;
            if (n < c.minX) {
                c.moveToX(c.minX)
            } else {
                if (n > c.maxX) {
                    c.moveToX(c.maxX)
                }
            }
            if (l < c.minY) {
                c.moveToY(c.minY)
            } else {
                if (l > c.maxY) {
                    c.moveToY(c.maxY)
                }
            }
            if (c.hasBeenDragged) {
                c.dragDone(j)
            }
        }
        if (p.hasBeenDragged) {
            p.dragDone(j)
        }
        this.locked = false;
        this.dO = null
    },keyPressed: function(a) {
        a = a || event;
        if (a.keyCode == 27) {
            if (this.dO) {
                this.dO.release(a)
            }
        }
    },toString: function() {
        return "[object DragHandlers]"
    }};
if (typeof window.CollectGarbage == "function") {
    (function() {
        var c = APE.drag, b = c.Draggable, a = b.instanceDestructor;
        APE.EventPublisher.get(window, "onunload").addAfter(a, b).add(a, c.DropTarget)
    })()
}
;
if (typeof Refresh == "undefined") {
    var Refresh = {}
}
if (!Refresh.Web) {
    Refresh.Web = {}
}
Refresh.Web.Color = function(b) {
    var a = {r: 0,g: 0,b: 0,h: 0,s: 0,v: 0,hex: "",setRgb: function(e, d, c) {
            this.r = e;
            this.g = d;
            this.b = c;
            var f = Refresh.Web.ColorMethods.rgbToHsv(this);
            this.h = f.h;
            this.s = f.s;
            this.v = f.v;
            this.hex = Refresh.Web.ColorMethods.rgbToHex(this)
        },setHsv: function(e, d, c) {
            this.h = e;
            this.s = d;
            this.v = c;
            var f = Refresh.Web.ColorMethods.hsvToRgb(this);
            this.r = f.r;
            this.g = f.g;
            this.b = f.b;
            this.hex = Refresh.Web.ColorMethods.rgbToHex(f)
        },setHex: function(c) {
            this.hex = c;
            var e = Refresh.Web.ColorMethods.hexToRgb(this.hex);
            this.r = e.r;
            this.g = e.g;
            this.b = e.b;
            var d = Refresh.Web.ColorMethods.rgbToHsv(e);
            this.h = d.h;
            this.s = d.s;
            this.v = d.v
        }};
    if (b) {
        if (b.hex) {
            a.setHex(b.hex)
        } else {
            if (b.r) {
                a.setRgb(b.r, b.g, b.b)
            } else {
                if (b.h) {
                    a.setHsv(b.h, b.s, b.v)
                }
            }
        }
    }
    return a
};
Refresh.Web.ColorMethods = {hexToRgb: function(e) {
        e = this.validateHex(e);
        var d = "00", c = "00", a = "00";
        if (e.length == 6) {
            d = e.substring(0, 2);
            c = e.substring(2, 4);
            a = e.substring(4, 6)
        } else {
            if (e.length > 4) {
                d = e.substring(4, e.length);
                e = e.substring(0, 4)
            }
            if (e.length > 2) {
                c = e.substring(2, e.length);
                e = e.substring(0, 2)
            }
            if (e.length > 0) {
                a = e.substring(0, e.length)
            }
        }
        return {r: this.hexToInt(d),g: this.hexToInt(c),b: this.hexToInt(a)}
    },validateHex: function(a) {
        a = new String(a).toUpperCase();
        a = a.replace(/[^A-F0-9]/g, "0");
        if (a.length > 6) {
            a = a.substring(0, 6)
        }
        return a
    },webSafeDec: function(a) {
        a = Math.round(a / 51);
        a *= 51;
        return a
    },hexToWebSafe: function(e) {
        var d, c, a;
        if (e.length == 3) {
            d = e.substring(0, 1);
            c = e.substring(1, 1);
            a = e.substring(2, 1)
        } else {
            d = e.substring(0, 2);
            c = e.substring(2, 4);
            a = e.substring(4, 6)
        }
        return intToHex(this.webSafeDec(this.hexToInt(d))) + this.intToHex(this.webSafeDec(this.hexToInt(c))) + this.intToHex(this.webSafeDec(this.hexToInt(a)))
    },rgbToWebSafe: function(a) {
        return {r: this.webSafeDec(a.r),g: this.webSafeDec(a.g),b: this.webSafeDec(a.b)}
    },rgbToHex: function(a) {
        return this.intToHex(a.r) + this.intToHex(a.g) + this.intToHex(a.b)
    },intToHex: function(b) {
        var a = (parseInt(b).toString(16));
        if (a.length == 1) {
            a = ("0" + a)
        }
        return a
    },hexToInt: function(a) {
        return (parseInt(a, 16))
    },rgbToHsv: function(e) {
        var i = e.r / 255, h = e.g / 255, c = e.b / 255, d = {h: 0,s: 0,v: 0}, f = 0, a = 0;
        if (i >= h && i >= c) {
            a = i;
            f = (h > c) ? c : h
        } else {
            if (h >= c && h >= i) {
                a = h;
                f = (i > c) ? c : i
            } else {
                a = c;
                f = (h > i) ? i : h
            }
        }
        d.v = a;
        d.s = (a) ? ((a - f) / a) : 0;
        if (!d.s) {
            d.h = 0
        } else {
            var j = a - f;
            if (i == a) {
                d.h = (h - c) / j
            } else {
                if (h == a) {
                    d.h = 2 + (c - i) / j
                } else {
                    d.h = 4 + (i - h) / j
                }
            }
            d.h = parseInt(d.h * 60);
            if (d.h < 0) {
                d.h += 360
            }
        }
        d.s = parseInt(d.s * 100);
        d.v = parseInt(d.v * 100);
        return d
    },hsvToRgb: function(e) {
        var j = {r: 0,g: 0,b: 0};
        var d = e.h;
        var m = e.s;
        var k = e.v;
        if (m == 0) {
            if (k == 0) {
                j.r = j.g = j.b = 0
            } else {
                j.r = j.g = j.b = parseInt(k * 255 / 100)
            }
        } else {
            if (d == 360) {
                d = 0
            }
            d /= 60;
            m = m / 100;
            k = k / 100;
            var c = parseInt(d);
            var g = d - c;
            var b = k * (1 - m);
            var a = k * (1 - (m * g));
            var l = k * (1 - (m * (1 - g)));
            switch (c) {
                case 0:
                    j.r = k;
                    j.g = l;
                    j.b = b;
                    break;
                case 1:
                    j.r = a;
                    j.g = k;
                    j.b = b;
                    break;
                case 2:
                    j.r = b;
                    j.g = k;
                    j.b = l;
                    break;
                case 3:
                    j.r = b;
                    j.g = a;
                    j.b = k;
                    break;
                case 4:
                    j.r = l;
                    j.g = b;
                    j.b = k;
                    break;
                case 5:
                    j.r = k;
                    j.g = b;
                    j.b = a;
                    break
            }
            j.r = parseInt(j.r * 255);
            j.g = parseInt(j.g * 255);
            j.b = parseInt(j.b * 255)
        }
        return j
    }};
if (!window.Refresh) {
    window.Refresh = {}
}
if (!window.Refresh.Web) {
    window.Refresh.Web = {}
}
Refresh.Web.ColorValuePicker = Class.create({initialize: function(a) {
        this.id = a;
        this.onValuesChanged = null;
        this._controls = $(a + "_Controls");
        this._hueInput = $(a + "_Hue") || {};
        this._valueInput = $(a + "_Brightness") || {};
        this._saturationInput = $(a + "_Saturation") || {};
        this._redInput = $(a + "_Red") || {};
        this._greenInput = $(a + "_Green") || {};
        this._blueInput = $(a + "_Blue") || {};
        this._hexInput = $(a + "_Hex") || {};
        this._event_onHsvKeyUp = this._onHsvKeyUp.bind(this);
        this._event_onHsvBlur = this._onHsvBlur.bind(this);
        this._event_onRgbKeyUp = this._onRgbKeyUp.bind(this);
        this._event_onRgbBlur = this._onRgbBlur.bind(this);
        this._event_onHexKeyUp = this._onHexKeyUp.bind(this);
        if (this._hexInput.observe) {
            this._hexInput.observe("keyup", this._event_onHexKeyUp)
        }
        this.color = new Refresh.Web.Color();
        if (this._hexInput) {
            if (this._hexInput.value != "") {
                this.color.setHex(this._hexInput.value)
            }
            this._hexInput.value = this.color.hex
        }
        if (this._redInput) {
            this._redInput.value = this.color.r
        }
        if (this._greenInput) {
            this._greenInput.value = this.color.g
        }
        if (this._blueInput) {
            this._blueInput.value = this.color.b
        }
        if (this._hueInput) {
            this._hueInput.value = this.color.h
        }
        if (this._saturationInput) {
            this._saturationInput.value = this.color.s
        }
        if (this._valueInput) {
            this._valueInput.value = this.color.v
        }
    },_onHsvKeyUp: function(a) {
        if (a.target.value == "") {
            return
        }
        this.validateHsv(a);
        this.setValuesFromHsvInputs();
        if (this.onValuesChanged) {
            this.onValuesChanged(this)
        }
    },_onRgbKeyUp: function(a) {
        if (a.target.value == "") {
            return
        }
        this.validateRgb(a);
        this.setValuesFromRgbInputs();
        if (this.onValuesChanged) {
            this.onValuesChanged(this)
        }
    },_onHexKeyUp: function(b, a) {
        if (b.target.value == "" || b.target.value === "transparent") {
            return
        }
        this.validateHex(b);
        this.setValuesFromHexInputs();
        if (this.onValuesChanged) {
            this.onValuesChanged(this, a)
        }
        if (b.keyCode === Event.KEY_RETURN) {
            this.parentInstance.updateVisualsAndNotify()
        }
    },_onHsvBlur: function(a) {
        if (a.target.value == "") {
            this.setValuesFromRgbInputs()
        }
    },_onRgbBlur: function(a) {
        if (a.target.value == "") {
            this.setValuesFromHsvInputs()
        }
    },HexBlur: function(a) {
        if (a.target.value == "") {
            this.setValuesFromHsvInputs()
        }
    },validateRgb: function(a) {
        if (!this._keyNeedsValidation(a)) {
            return a
        }
        this._redInput.value = this._setValueInRange(this._redInput.value, 0, 255);
        this._greenInput.value = this._setValueInRange(this._greenInput.value, 0, 255);
        this._blueInput.value = this._setValueInRange(this._blueInput.value, 0, 255)
    },validateHsv: function(a) {
        if (!this._keyNeedsValidation(a)) {
            return a
        }
        this._hueInput.value = this._setValueInRange(this._hueInput.value, 0, 359);
        this._saturationInput.value = this._setValueInRange(this._saturationInput.value, 0, 100);
        this._valueInput.value = this._setValueInRange(this._valueInput.value, 0, 100)
    },validateHex: function(c) {
        if (!this._keyNeedsValidation(c)) {
            return c
        }
        var a = this._hexInput.value;
        var b = a.replace(/[^A-Fa-f0-9]/g, "0");
        if (b.length > 6) {
            b = b.substring(0, 6)
        }
        b = Number.prototype.toPaddedString.call(b, 6);
        this._normalizedHexValue = b
    },_keyNeedsValidation: function(a) {
        if (a.keyCode == 9 || a.keyCode == 16 || a.keyCode == 38 || a.keyCode == 29 || a.keyCode == 40 || a.keyCode == 37 || ((a.ctrlKey || a.metaKey) && (a.keyCode == "c".charCodeAt() || a.keyCode == "v".charCodeAt()))) {
            return false
        }
        return true
    },_setValueInRange: function(c, b, a) {
        if (c == "" || isNaN(c)) {
            return b
        }
        c = parseInt(c);
        if (c > a) {
            return a
        }
        if (c < b) {
            return b
        }
        return c
    },setValuesFromRgb: function(d, c, a) {
        this.color.setRgb(d, c, a);
        this._hexInput.value = this.color.hex;
        this._hueInput.value = this.color.h;
        this._saturationInput.value = this.color.s;
        this._valueInput.value = this.color.v
    },setValuesFromHsv: function(c, b, a) {
        this.color.setHsv(c, b, a);
        this._hexInput.value = this.color.hex;
        this._redInput.value = this.color.r;
        this._greenInput.value = this.color.g;
        this._blueInput.value = this.color.b
    },setValuesFromHex: function(a) {
        this.color.setHex(a);
        this._redInput.value = this.color.r;
        this._greenInput.value = this.color.g;
        this._blueInput.value = this.color.b;
        this._hueInput.value = this.color.h;
        this._saturationInput.value = this.color.s;
        this._valueInput.value = this.color.v;
        this._hexInput.value = a
    },setValuesFromRgbInputs: function() {
        this.setValuesFromRgb(this._redInput.value, this._greenInput.value, this._blueInput.value)
    },setValuesFromHsvInputs: function() {
        this.setValuesFromHsv(this._hueInput.value, this._saturationInput.value, this._valueInput.value)
    },setValuesFromHexInputs: function() {
        this.setValuesFromHex(this._normalizedHexValue)
    }});
if (typeof Refresh == "undefined") {
    var Refresh = {}
}
if (!Refresh.Web) {
    Refresh.Web = {}
}
Refresh.Web.SlidersList = [];
Refresh.Web.DefaultSliderSettings = {xMinValue: 0,xMaxValue: 100,yMinValue: 0,yMaxValue: 100,arrowImage: "refresh_web/colorpicker/images/rangearrows.gif"};
Refresh.Web.Slider = Class.create({_bar: null,_arrow: null,initialize: function(c, a) {
        this.id = c;
        this.settings = Object.extend(Object.clone(Refresh.Web.DefaultSliderSettings), a || {});
        this.xValue = 0;
        this.yValue = 0;
        this._bar = $(this.id);
        this._arrow = document.createElement("img");
        this._arrow.border = 0;
        this._arrow.src = this.settings.arrowImage;
        this._arrow.margin = 0;
        this._arrow.padding = 0;
        this._arrow.style.position = "absolute";
        this._arrow.style.top = "0px";
        this._arrow.style.left = "0px";
        this.settings.container.appendChild(this._arrow);
        var b = this;
        this.setPositioningVariables();
        this._event_docMouseMove = this._docMouseMove.bind(this);
        this._event_docMouseUp = this._docMouseUp.bind(this);
        Event.observe(this._bar, "mousedown", this._bar_mouseDown.bind(this));
        Event.observe(this._arrow, "mousedown", this._arrow_mouseDown.bind(this));
        this.setArrowPositionFromValues();
        if (this.onValuesChanged) {
            this.onValuesChanged(this)
        }
        Refresh.Web.SlidersList.push(this)
    },setPositioningVariables: function() {
        this._barWidth = this._bar.getWidth();
        this._barHeight = this._bar.getHeight();
        var a = this._bar.cumulativeOffset();
        this._barTop = a.top;
        this._barLeft = a.left;
        this._barBottom = this._barTop + this._barHeight;
        this._barRight = this._barLeft + this._barWidth;
        this._arrow = $(this._arrow);
        this._arrowWidth = this._arrow.getWidth();
        this._arrowHeight = this._arrow.getHeight();
        this.MinX = this._barLeft;
        this.MinY = this._barTop;
        this.MaxX = this._barRight;
        this.MinY = this._barBottom
    },setArrowPositionFromValues: function(g) {
        this.setPositioningVariables();
        var b = 0, a = 0;
        if (this.settings.xMinValue != this.settings.xMaxValue) {
            if (this.xValue == this.settings.xMinValue) {
                b = 0
            } else {
                if (this.xValue == this.settings.xMaxValue) {
                    b = this._barWidth - 1
                } else {
                    var h = this.settings.xMaxValue;
                    if (this.settings.xMinValue < 1) {
                        h = h + Math.abs(this.settings.xMinValue) + 1
                    }
                    var d = this.xValue;
                    if (this.xValue < 1) {
                        d = d + 1
                    }
                    b = d / h * this._barWidth;
                    if (parseInt(b) == (h - 1)) {
                        b = h
                    } else {
                        b = parseInt(b)
                    }
                    if (this.settings.xMinValue < 1) {
                        b = b - Math.abs(this.settings.xMinValue) - 1
                    }
                }
            }
        }
        if (this.settings.yMinValue != this.settings.yMaxValue) {
            if (this.yValue == this.settings.yMinValue) {
                a = 0
            } else {
                if (this.yValue == this.settings.yMaxValue) {
                    a = this._barHeight - 1
                } else {
                    var f = this.settings.yMaxValue;
                    if (this.settings.yMinValue < 1) {
                        f = f + Math.abs(this.settings.yMinValue) + 1
                    }
                    var c = this.yValue;
                    if (this.yValue < 1) {
                        c = c + 1
                    }
                    var a = c / f * this._barHeight;
                    if (parseInt(a) == (f - 1)) {
                        a = f
                    } else {
                        a = parseInt(a)
                    }
                    if (this.settings.yMinValue < 1) {
                        a = a - Math.abs(this.settings.yMinValue) - 1
                    }
                }
            }
        }
        this._setArrowPosition(b, a)
    },_setArrowPosition: function(a, d) {
        if (a < 0) {
            a = 0
        }
        if (a > this._barWidth) {
            a = this._barWidth
        }
        if (d < 0) {
            d = 0
        }
        if (d > this._barHeight) {
            d = this._barHeight
        }
        var c = a;
        var b = d;
        if (this._arrowWidth > this._barWidth) {
            c = c - (this._arrowWidth / 2 - this._barWidth / 2)
        } else {
            c = c - parseInt(this._arrowWidth / 2)
        }
        if (this._arrowHeight > this._barHeight) {
            b = b - (this._arrowHeight / 2 - this._barHeight / 2)
        } else {
            b = b - parseInt(this._arrowHeight / 2)
        }
        this._arrow.style.left = c + "px";
        this._arrow.style.top = b + "px"
    },_bar_mouseDown: function(a) {
        this._mouseDown(a)
    },_arrow_mouseDown: function(a) {
        this._mouseDown(a)
    },_mouseDown: function(a) {
        if (this._colorsDisabled) {
            return
        }
        document.fire(Refresh.Web.ColorPicker.COLOR_WILL_CHANGE_EVENT);
        Refresh.Web.ActiveSlider = this;
        this.setValuesFromMousePosition(a);
        Event.observe(document, "mousemove", this._event_docMouseMove);
        Event.observe(document, "mouseup", this._event_docMouseUp);
        Event.stop(a)
    },_docMouseMove: function(a) {
        this.setValuesFromMousePosition(a)
    },_docMouseUp: function(a) {
        Event.stopObserving(document, "mousemove", this._event_docMouseMove);
        Event.stopObserving(document, "mouseup", this._event_docMouseUp);
        Event.stop(a);
        this._bar.fire("color:selected")
    },setValuesFromMousePosition: function(g) {
        var a = Event.pointer(g), d = 0, b = 0;
        if (a.x < this._barLeft) {
            d = 0
        } else {
            if (a.x > this._barRight) {
                d = this._barWidth
            } else {
                d = a.x - this._barLeft + 1
            }
        }
        if (a.y < this._barTop) {
            b = 0
        } else {
            if (a.y > this._barBottom) {
                b = this._barHeight
            } else {
                b = a.y - this._barTop + 1
            }
        }
        var f = parseInt(d / this._barWidth * this.settings.xMaxValue), c = parseInt(b / this._barHeight * this.settings.yMaxValue);
        this.xValue = f;
        this.yValue = c;
        if (this.settings.xMaxValue == this.settings.xMinValue) {
            d = 0
        }
        if (this.settings.yMaxValue == this.settings.yMinValue) {
            b = 0
        }
        this._setArrowPosition(d, b);
        if (this.onValuesChanged) {
            this.onValuesChanged(this)
        }
    }});
if (typeof Refresh == "undefined") {
    var Refresh = {}
}
if (typeof Refresh.Web == "undefined") {
    Refresh.Web = {}
}
Refresh.Web.ColorPicker = Class.create({defaultOptions: {startMode: "h",startHex: "ff0000",clientFilesPath: "http://assets.printio.bitsonnet.com/images/colorpicker/",sliderWidth: 20,canvasWidth: 256,canvasHeight: 256},hasFilterButNoOpacity: (function() {
        var a = document.documentElement;
        return (a && a.style && typeof a.style.opacity != "string" && typeof a.style.filter == "string")
    })(),initialize: function(c, a) {
        this.id = c;
        this.settings = Object.extend(Object.clone(this.defaultOptions), a || {});
        this._container = $(this.id + "_Container");
        this._controls = $(this.id + "_Controls");
        this._hueRadio = $(this.id + "_HueRadio");
        this._saturationRadio = $(this.id + "_SaturationRadio");
        this._valueRadio = $(this.id + "_BrightnessRadio");
        this._redRadio = $(this.id + "_RedRadio");
        this._greenRadio = $(this.id + "_GreenRadio");
        this._blueRadio = $(this.id + "_BlueRadio");
        if (this._hueRadio) {
            this._hueRadio.value = "h"
        }
        if (this._saturationRadio) {
            this._saturationRadio.value = "s"
        }
        if (this._valueRadio) {
            this._valueRadio.value = "v"
        }
        if (this._redRadio) {
            this._redRadio.value = "r"
        }
        if (this._greenRadio) {
            this._greenRadio.value = "g"
        }
        if (this._blueRadio) {
            this._blueRadio.value = "b"
        }
        if (this._controls) {
            Event.observe(this._controls, "click", function(f, d) {
                if (d = f.findElement('input[type="radio"]')) {
                    this.setColorMode(d.value)
                }
            }.bind(this))
        }
        this._preview = $(this.id + "_Preview");
        this._mapBase = $(this.id + "_ColorMap");
        this._mapBase.style.width = this.settings.canvasWidth + "px";
        this._mapBase.style.height = this.settings.canvasHeight + "px";
        this._mapBase.style.padding = 0;
        this._mapBase.style.margin = 0;
        this._mapBase.style.position = "relative";
        this._mapL1 = new Element("img", {src: this.settings.clientFilesPath + "blank.gif",width: this.settings.canvasWidth,height: this.settings.canvasHeight});
        this._mapL1.style.margin = "0";
        this._mapBase.appendChild(this._mapL1);
        this._mapL2 = new Element("img", {src: this.settings.clientFilesPath + "blank.gif",width: this.settings.canvasWidth,height: this.settings.canvasHeight});
        this._mapBase.appendChild(this._mapL2);
        this._mapL2.style.clear = "both";
        this._mapL2.style.position = "absolute";
        this._mapL2.style.top = "0";
        this._mapL2.style.left = "0";
        this._mapL2.setOpacity(0.5);
        this._bar = $(this.id + "_ColorBar");
        this._bar.style.width = this.defaultOptions.sliderWidth + "px";
        this._bar.style.height = this.settings.canvasHeight + "px";
        this._barL1 = new Element("img", {src: this.settings.clientFilesPath + "blank.gif",width: this.settings.sliderWidth,height: this.settings.canvasHeight});
        this._barL1.style.margin = "0";
        this._barL1.style.position = "absolute";
        this._barL1.style.top = "0";
        this._barL1.style.left = "0";
        this._bar.appendChild(this._barL1);
        this._barL2 = new Element("img", {src: this.settings.clientFilesPath + "blank.gif",width: this.settings.sliderWidth,height: this.settings.canvasHeight});
        this._barL2.style.position = "absolute";
        this._barL2.style.top = "0";
        this._barL2.style.left = "0";
        this._bar.appendChild(this._barL2);
        this._barL3 = new Element("img", {src: this.settings.clientFilesPath + "blank.gif",width: this.settings.sliderWidth,height: this.settings.canvasHeight});
        this._barL3.style.position = "absolute";
        this._barL3.style.top = "0";
        this._barL3.style.left = "0";
        this._barL3.style.backgroundColor = "#ff0000";
        this._bar.appendChild(this._barL3);
        this._barL4 = new Element("img", {src: this.settings.clientFilesPath + "bar-brightness.png",width: this.settings.sliderWidth,height: this.settings.canvasHeight});
        this._barL4.style.position = "absolute";
        this._barL4.style.top = "0";
        this._barL4.style.left = "0";
        this._bar.appendChild(this._barL4);
        this._map = new Refresh.Web.Slider(this._mapL2, {xMaxValue: 255,yMinValue: 255,arrowImage: this.settings.clientFilesPath + "mappoint.gif",container: this._mapBase});
        this._slider = new Refresh.Web.Slider(this._barL4, {xMinValue: 1,xMaxValue: 1,yMinValue: 255,arrowImage: this.settings.clientFilesPath + "rangearrows.gif",container: this._bar});
        this._cvp = new Refresh.Web.ColorValuePicker(this.id);
        this._cvp.parentInstance = this;
        var b = this;
        this._slider.onValuesChanged = function() {
            b.sliderValueChanged()
        };
        this._map.onValuesChanged = function() {
            b.mapValueChanged()
        };
        this._cvp.onValuesChanged = function(d) {
            b.textValuesChanged(d)
        };
        this.setColorMode(this.settings.startMode);
        if (this.settings.startHex && this._cvp._hexInput) {
            this._cvp._hexInput.value = this.settings.startHex
        }
        this._cvp.setValuesFromHex();
        this.positionMapAndSliderArrows();
        this.updateVisualsAndNotify();
        this.color = null
    },reflow: function() {
        var a = this._cvp.color;
        a.h = a.h === 0 ? 1 : a.h;
        this.updateVisuals();
        var b = this;
        setTimeout(function() {
            b.positionMapAndSliderArrows()
        }, 50)
    },setTransparent: function() {
        this._mapBase.down("img", 0).setOpacity(0);
        this._mapBase.down("img", 1).setOpacity(0);
        this._mapBase.down("img", 2).setOpacity(0);
        this._bar.down("img", 3).setOpacity(0);
        this._bar.down("img", 4).setOpacity(0);
        this._map._colorsDisabled = true;
        this._slider._colorsDisabled = true;
        this._preview.style.backgroundColor = "transparent"
    },unsetTransparent: function() {
        this._mapBase.down("img", 0).setOpacity(1);
        this._mapBase.down("img", 1).setOpacity(1);
        this._mapBase.down("img", 2).setOpacity(1);
        this._bar.down("img", 3).setOpacity(1);
        this._bar.down("img", 4).setOpacity(1);
        this._map._colorsDisabled = false;
        this._slider._colorsDisabled = false;
        this._preview.style.backgroundColor = "transparent"
    },show: function() {
        this._map.setPositioningVariables();
        this._slider.setPositioningVariables();
        this.positionMapAndSliderArrows();
        this._container.style.visibility = "visible"
    },hide: function() {
        this._container.style.visibility = "hidden"
    },_onWebSafeClicked: function(a) {
        this.setColorMode(this.ColorMode)
    },textValuesChanged: function(a) {
        this.positionMapAndSliderArrows();
        a ? this.updateVisuals() : this.updateVisualsAndNotify()
    },setColorMode: function(b) {
        this.color = this._cvp.color;
        function a(d, c) {
            d.setAlpha(c, 100);
            c.style.backgroundColor = "";
            c.src = d.settings.clientFilesPath + "blank.gif";
            c.style.filter = ""
        }
        a(this, this._mapL1);
        a(this, this._mapL2);
        a(this, this._barL1);
        a(this, this._barL2);
        a(this, this._barL3);
        a(this, this._barL4);
        if (this.settings.controlsOn) {
            this._hueRadio.checked = false;
            this._saturationRadio.checked = false;
            this._valueRadio.checked = false;
            this._redRadio.checked = false;
            this._greenRadio.checked = false;
            this._blueRadio.checked = false
        }
        switch (b) {
            case "h":
                if (this._hueRadio) {
                    this._hueRadio.checked = true
                }
                this._mapL1.style.backgroundColor = "#" + ((typeof this.color.hex === "undefined" || this.color.hex === "") ? "ffffff" : this.color.hex);
                this._mapL2.style.backgroundColor = "transparent";
                this.setImg(this._mapL2, this.settings.clientFilesPath + "map-hue.png");
                this.setAlpha(this._mapL2, 100);
                this.setImg(this._barL4, this.settings.clientFilesPath + "bar-hue.png");
                this._map.settings.xMaxValue = 100;
                this._map.settings.yMaxValue = 100;
                this._slider.settings.yMaxValue = 359;
                break;
            case "s":
                if (this._saturationRadio) {
                    this._saturationRadio.checked = true
                }
                this.setImg(this._mapL1, this.settings.clientFilesPath + "map-saturation.png");
                this.setImg(this._mapL2, this.settings.clientFilesPath + "map-saturation-overlay.png");
                this.setAlpha(this._mapL2, 0);
                this.setBG(this._barL3, this.color.hex);
                this.setImg(this._barL4, this.settings.clientFilesPath + "bar-saturation.png");
                this._map.settings.xMaxValue = 359;
                this._map.settings.yMaxValue = 100;
                this._slider.settings.yMaxValue = 100;
                break;
            case "v":
                if (this._valueRadio) {
                    this._valueRadio.checked = true
                }
                this.setBG(this._mapL1, "000");
                this.setImg(this._mapL2, this.settings.clientFilesPath + "map-brightness.png");
                this._barL3.style.backgroundColor = "#" + this.color.hex;
                this.setImg(this._barL4, this.settings.clientFilesPath + "bar-brightness.png");
                this._map.settings.xMaxValue = 359;
                this._map.settings.yMaxValue = 100;
                this._slider.settings.yMaxValue = 100;
                break;
            case "r":
                this._redRadio.checked = true;
                this.setImg(this._mapL2, this.settings.clientFilesPath + "map-red-max.png");
                this.setImg(this._mapL1, this.settings.clientFilesPath + "map-red-min.png");
                this.setImg(this._barL4, this.settings.clientFilesPath + "bar-red-tl.png");
                this.setImg(this._barL3, this.settings.clientFilesPath + "bar-red-tr.png");
                this.setImg(this._barL2, this.settings.clientFilesPath + "bar-red-br.png");
                this.setImg(this._barL1, this.settings.clientFilesPath + "bar-red-bl.png");
                break;
            case "g":
                this._greenRadio.checked = true;
                this.setImg(this._mapL2, this.settings.clientFilesPath + "map-green-max.png");
                this.setImg(this._mapL1, this.settings.clientFilesPath + "map-green-min.png");
                this.setImg(this._barL4, this.settings.clientFilesPath + "bar-green-tl.png");
                this.setImg(this._barL3, this.settings.clientFilesPath + "bar-green-tr.png");
                this.setImg(this._barL2, this.settings.clientFilesPath + "bar-green-br.png");
                this.setImg(this._barL1, this.settings.clientFilesPath + "bar-green-bl.png");
                break;
            case "b":
                this._blueRadio.checked = true;
                this.setImg(this._mapL2, this.settings.clientFilesPath + "map-blue-max.png");
                this.setImg(this._mapL1, this.settings.clientFilesPath + "map-blue-min.png");
                this.setImg(this._barL4, this.settings.clientFilesPath + "bar-blue-tl.png");
                this.setImg(this._barL3, this.settings.clientFilesPath + "bar-blue-tr.png");
                this.setImg(this._barL2, this.settings.clientFilesPath + "bar-blue-br.png");
                this.setImg(this._barL1, this.settings.clientFilesPath + "bar-blue-bl.png");
                break;
            default:
                alert("invalid mode");
                break
        }
        switch (b) {
            case "h":
            case "s":
            case "v":
                this._map.settings.xMinValue = 1;
                this._map.settings.yMinValue = 1;
                this._slider.settings.yMinValue = 1;
                break;
            case "r":
            case "g":
            case "b":
                this._map.settings.xMinValue = 0;
                this._map.settings.yMinValue = 0;
                this._slider.settings.yMinValue = 0;
                this._map.settings.xMaxValue = 255;
                this._map.settings.yMaxValue = 255;
                this._slider.settings.yMaxValue = 255;
                break
        }
        this.ColorMode = b;
        this.positionMapAndSliderArrows();
        this.updateMapVisuals();
        this.updateSliderVisuals()
    },mapValueChanged: function() {
        switch (this.ColorMode) {
            case "h":
                this._cvp._saturationInput.value = this._map.xValue;
                this._cvp._valueInput.value = 100 - this._map.yValue;
                break;
            case "s":
                this._cvp._hueInput.value = this._map.xValue;
                this._cvp._valueInput.value = 100 - this._map.yValue;
                break;
            case "v":
                this._cvp._hueInput.value = this._map.xValue;
                this._cvp._saturationInput.value = 100 - this._map.yValue;
                break;
            case "r":
                this._cvp._blueInput.value = this._map.xValue;
                this._cvp._greenInput.value = 256 - this._map.yValue;
                break;
            case "g":
                this._cvp._blueInput.value = this._map.xValue;
                this._cvp._redInput.value = 256 - this._map.yValue;
                break;
            case "b":
                this._cvp._redInput.value = this._map.xValue;
                this._cvp._greenInput.value = 256 - this._map.yValue;
                break
        }
        switch (this.ColorMode) {
            case "h":
            case "s":
            case "v":
                this._cvp.setValuesFromHsvInputs();
                break;
            case "r":
            case "g":
            case "b":
                this._cvp.setValuesFromRgbInputs();
                break
        }
        this.updateVisualsAndNotify()
    },sliderValueChanged: function() {
        switch (this.ColorMode) {
            case "h":
                this._cvp._hueInput.value = 360 - this._slider.yValue;
                break;
            case "s":
                this._cvp._saturationInput.value = 100 - this._slider.yValue;
                break;
            case "v":
                this._cvp._valueInput.value = 100 - this._slider.yValue;
                break;
            case "r":
                this._cvp._redInput.value = 255 - this._slider.yValue;
                break;
            case "g":
                this._cvp._greenInput.value = 255 - this._slider.yValue;
                break;
            case "b":
                this._cvp._blueInput.value = 255 - this._slider.yValue;
                break
        }
        switch (this.ColorMode) {
            case "h":
            case "s":
            case "v":
                this._cvp.setValuesFromHsvInputs();
                break;
            case "r":
            case "g":
            case "b":
                this._cvp.setValuesFromRgbInputs();
                break
        }
        this.updateVisualsAndNotify()
    },positionMapAndSliderArrows: function() {
        this.color = this._cvp.color;
        var b = 0;
        switch (this.ColorMode) {
            case "h":
                b = 360 - this.color.h;
                break;
            case "s":
                b = 100 - this.color.s;
                break;
            case "v":
                b = 100 - this.color.v;
                break;
            case "r":
                b = 255 - this.color.r;
                break;
            case "g":
                b = 255 - this.color.g;
                break;
            case "b":
                b = 255 - this.color.b;
                break
        }
        this._slider.yValue = b;
        this._slider.setArrowPositionFromValues();
        var a = 0, c = 0;
        switch (this.ColorMode) {
            case "h":
                a = this.color.s;
                c = 100 - this.color.v;
                break;
            case "s":
                a = this.color.h;
                c = 100 - this.color.v;
                break;
            case "v":
                a = this.color.h;
                c = 100 - this.color.s;
                break;
            case "r":
                a = this.color.b;
                c = 256 - this.color.g;
                break;
            case "g":
                a = this.color.b;
                c = 256 - this.color.r;
                break;
            case "b":
                a = this.color.r;
                c = 256 - this.color.g;
                break
        }
        this._map.xValue = a;
        this._map.yValue = c;
        this._map.setArrowPositionFromValues()
    },updateVisuals: function() {
        this.updatePreview();
        this.updateMapVisuals();
        this.updateSliderVisuals()
    },updateVisualsAndNotify: function() {
        this.updateVisuals();
        document.fire("color:did:change", {color: this._cvp.color,instance: this})
    },updatePreview: function() {
        var b = this._cvp.color, a = b.hex || Refresh.Web.ColorMethods.rgbToHex(b);
        this.setPreviewColorFromHex(a)
    },updateMapVisuals: function() {
        this.color = this._cvp.color;
        switch (this.ColorMode) {
            case "h":
                var a = new Refresh.Web.Color({h: this.color.h,s: 100,v: 100});
                this.setBG(this._mapL1, a.hex);
                break;
            case "s":
                this.setAlpha(this._mapL2, 100 - this.color.s);
                break;
            case "v":
                this.setAlpha(this._mapL2, this.color.v);
                break;
            case "r":
                this.setAlpha(this._mapL2, this.color.r / 256 * 100);
                break;
            case "g":
                this.setAlpha(this._mapL2, this.color.g / 256 * 100);
                break;
            case "b":
                this.setAlpha(this._mapL2, this.color.b / 256 * 100);
                break
        }
    },updateSliderVisuals: function() {
        this.color = this._cvp.color;
        switch (this.ColorMode) {
            case "h":
                break;
            case "s":
                var a = new Refresh.Web.Color({h: this.color.h + "",s: this.color.s + "",v: this.color.v + ""});
                this.setBG(this._barL3, a.hex);
                break;
            case "v":
                var e = new Refresh.Web.Color({h: this.color.h,s: this.color.s,v: 100});
                this.setBG(this._barL3, e.hex);
                break;
            case "r":
            case "g":
            case "b":
                var h = 0, c = 0;
                if (this.ColorMode == "r") {
                    h = this._cvp._blueInput.value;
                    c = this._cvp._greenInput.value
                } else {
                    if (this.ColorMode == "g") {
                        h = this._cvp._blueInput.value;
                        c = this._cvp._redInput.value
                    } else {
                        if (this.ColorMode == "b") {
                            h = this._cvp._redInput.value;
                            c = this._cvp._greenInput.value
                        }
                    }
                }
                var b = (h / 256) * 100, d = (c / 256) * 100, g = ((256 - h) / 256) * 100, f = ((256 - c) / 256) * 100;
                this.setAlpha(this._barL4, (d > g) ? g : d);
                this.setAlpha(this._barL3, (d > b) ? b : d);
                this.setAlpha(this._barL2, (f > b) ? b : f);
                this.setAlpha(this._barL1, (f > g) ? g : f);
                break
        }
    },setBG: function(a, d) {
        try {
            a.style.backgroundColor = "#" + d
        } catch (b) {
        }
    },setImg: function(a, b) {
        if (b.indexOf("png") && this.hasFilterButNoOpacity) {
            a.pngSrc = b;
            a.src = this.settings.clientFilesPath + "blank.gif";
            a.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + b + "');"
        } else {
            a.src = b
        }
    },setAlpha: function(b, a) {
        if (this.hasFilterButNoOpacity) {
            var c = b.pngSrc;
            if (c != null && c.indexOf("map-hue") == -1) {
                b.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + c + "') progid:DXImageTransform.Microsoft.Alpha(opacity=" + a + ")"
            }
        } else {
            b.setOpacity(a / 100)
        }
    },setValueFromHex: function(b, a) {
        this.unsetTransparent();
        this._cvp._hexInput.value = b;
        this._cvp._onHexKeyUp({target: {value: b},keyCode: 100}, a)
    },setValueFromRgb: function(d, c, a) {
        this._cvp.setValuesFromRgb(d, c, a);
        this.updateVisuals();
        this._cvp.onValuesChanged(this._cvp)
    },setPreviewColorFromHex: function(a) {
        this._preview.style.backgroundColor = "#" + a
    },setValueFromAny: function(b, a) {
        this.setValueFromHex(new fabric.Color(b).toHex(), a)
    },getControls: function() {
        return this._controls
    },getCurrentColor: function() {
        return this._cvp.color.hex
    }});
if (Object.isUndefined(Proto)) {
    var Proto = {}
}
Proto.Menu = Class.create((function() {
    var b = function() {
        return true
    }, c = Prototype.Browser.IE;
    var a = {selector: ".contextmenu",className: "protoMenu",pageOffset: 25,fade: false,zIndex: 100,beforeShow: b,beforeHide: b,beforeSelect: b};
    return {initialize: function() {
            this.options = Object.extend(Object.clone(a), arguments[0] || {});
            this.shim = new Element("iframe", {style: "position:absolute;filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);display:none",src: "javascript:false;",frameborder: 0});
            this.options.fade = this.options.fade && !Object.isUndefined(Effect);
            this.container = new Element("div", {className: this.options.className,style: "display:none"});
            this.container.setStyle({zIndex: this.options.zIndex});
            this.list = new Element("ul");
            this.options.menuItems.each(this.addItem, this);
            $(document.body).insert(this.container.insert(this.list).observe("contextmenu", Event.stop).observe("click", this.onClick.bind(this)));
            c && $(document.body).insert(this.shim);
            document.observe("click", function(d) {
                if (this.container.visible() && !d.isRightClick()) {
                    d.stop();
                    this.hide()
                }
            }.bind(this));
            document.observe(Prototype.Browser.Opera ? "click" : "contextmenu", function(d) {
                if (!d.findElement(this.options.selector)) {
                    return
                }
                if (Prototype.Browser.Opera && !d.ctrlKey) {
                    return
                }
                d.stop();
                this.show(d)
            }.bind(this))
        },addItem: function(d) {
            this.list.insert(new Element("li", {className: d.separator ? "separator" : ""}).insert(d.separator ? "" : Object.extend(new Element("a", {href: "#",title: "title" in d ? d.title : d.name,className: (d.className || "") + (d.disabled ? " disabled" : " enabled")}), {_callback: d.callback}).update(d.name)))
        },removeItem: function(d) {
            return this.getItem(d).remove()
        },getItem: function(d) {
            return this.list.childElements().find(function(e) {
                return e.innerHTML.indexOf(d) !== -1
            })
        },show: function(j) {
            j.stop();
            if (!this.options.beforeShow.call(this, j)) {
                this.hide();
                return
            }
            document.fire("menu:show", {instance: this});
            var k = Event.pointer(j), f = k.x, l = k.y, h = document.viewport.getDimensions(), i = document.viewport.getScrollOffsets(), d = this.container.getDimensions(), g = {left: ((f + d.width + this.options.pageOffset) > h.width ? (h.width - d.width - this.options.pageOffset) : f) + "px",top: ((l - i.top + d.height) > h.height && (l - i.top) > d.height ? (l - d.height) : l) + "px"};
            this.container.setStyle(g);
            if (c) {
                this.shim.setStyle(Object.extend(Object.extend(d, g), {zIndex: this.options.zIndex - 1})).show()
            }
            this.options.fade ? Effect.Appear(this.container, {duration: 0.25}) : this.container.show();
            this.event = j
        },hide: function(d) {
            this.options.beforeHide(d);
            if (c) {
                this.shim.hide()
            }
            this.container.hide()
        },onClick: function(f, d) {
            if ((d = f.findElement("li a")) && d.descendantOf(this.list)) {
                f.stop();
                if (d._callback && !d.hasClassName("disabled")) {
                    this.options.beforeSelect(f);
                    if (c) {
                        this.shim.hide()
                    }
                    this.container.hide();
                    d._callback(this.event)
                }
            }
        }}
})());
/*! Proto!MultiSelect Copyright: InteRiders <http://interiders.com/> - Distributed under MIT - Keep this message!  */
Object.extend(Event, {KEY_COMMA: {code: 188,value: ","},KEY_SPACE: {code: 32,value: " "}});
var ResizableTextbox = Class.create({initialize: function(b, a) {
        var c = this;
        this.options = $H({min: 5,max: 500,step: 7});
        this.options.update(a);
        this.el = $(b);
        this.width = this.el.offsetWidth;
        this.el.observe("keyup", function() {
            var d = c.options.get("step") * $F(this).length;
            if (d <= c.options.get("min")) {
                d = c.width
            }
            if (!($F(this).length == this.retrieveData("rt-value") || d <= c.options.min || d >= c.options.max)) {
                this.setStyle({width: d})
            }
        }).observe("keydown", function() {
            this.cacheData("rt-value", $F(this).length)
        })
    }});
var TextboxList = Class.create({initialize: function(b, a) {
        this.options = $H({resizable: {},className: "bit",separator: Event.KEY_COMMA,tabindex: null,extrainputs: true,startinput: true,hideempty: true,newValues: false,newValueDelimiters: ["[", "]"],spaceReplace: "",fetchFile: undefined,fetchMethod: "get",results: 10,maxResults: 0,wordMatch: false,onEmptyInput: function(c) {
            },caseSensitive: false,regexSearch: true});
        this.current_input = "";
        this.options.update(a);
        this.element = $(b).hide();
        this.bits = new Hash();
        this.events = new Hash();
        this.count = 0;
        this.current = false;
        this.maininput = this.createInput({className: "maininput"});
        this.maininput.addClassName("maininput");
        this.holder = new Element("ul", {className: "holder"}).insert(this.maininput);
        if (this.options.get("tabindex")) {
            this.maininput.down("input").writeAttribute("tabindex", this.options.get("tabindex"))
        }
        this.element.insert({before: this.holder});
        this.holder.observe("click", function(c) {
            c.stop();
            if (this.maininput != this.current) {
                this.focus(this.maininput)
            }
        }.bind(this));
        this.makeResizable(this.maininput);
        this.setEvents()
    },setEvents: function() {
        document.observe("keydown", function(a) {
            if (!this.current) {
                return
            }
            if (this.current.retrieveData("type") == "box" && a.keyCode == Event.KEY_BACKSPACE) {
                a.preventObjectRemoval = true;
                a.stop()
            }
        }.bind(this));
        document.observe("keyup", function(a) {
            a.stop();
            if (!this.current) {
                return
            }
            switch (a.keyCode) {
                case Event.KEY_LEFT:
                    return this.move("left");
                case Event.KEY_RIGHT:
                    return this.move("right");
                case Event.KEY_DELETE:
                case Event.KEY_BACKSPACE:
                    this.moveDispose()
            }
        }.bind(this)).observe("click", function() {
            document.fire("blur")
        }.bindAsEventListener(this))
    },update: function() {
        this.element.value = this.bits.values().join(this.options.get("separator").value);
        if (!this.current_input.blank()) {
            this.element.value += (this.element.value.blank() ? "" : this.options.get("separator").value) + this.current_input
        }
        return this
    },add: function(c, a) {
        var d = this.id_base + "-" + this.count++;
        var b = this.createBox($pick(a, c), {id: d,"class": this.options.get("className"),newValue: c.newValue ? "true" : "false"});
        (this.current || this.maininput).insert({before: b});
        b.observe("click", function(f) {
            f.stop();
            this.focus(b)
        }.bind(this));
        this.bits.set(d, c.value);
        this.update();
        if (this.options.get("extrainputs") && (this.options.get("startinput") || b.previous())) {
            this.addSmallInput(b, "before")
        }
        return b
    },addSmallInput: function(c, b) {
        var a = this.createInput({"class": "smallinput"});
        c.insert({}[b] = a);
        a.cacheData("small", true);
        this.makeResizable(a);
        if (this.options.get("hideempty")) {
            a.hide()
        }
        return a
    },dispose: function(a) {
        this.bits.unset(a.id);
        this.update();
        if (a.previous() && a.previous().retrieveData("small")) {
            a.previous().remove()
        }
        if (this.current == a) {
            this.focus(a.next())
        }
        if (a.retrieveData("type") == "box") {
            a.onBoxDispose(this)
        }
        a.remove();
        return this
    },focus: function(b, a) {
        if (!this.current) {
            b.fire("focus")
        } else {
            if (this.current == b) {
                return this
            }
        }
        this.blur();
        b.addClassName(this.options.get("className") + "-" + b.retrieveData("type") + "-focus");
        if (b.retrieveData("small")) {
            b.setStyle({display: "block"})
        }
        if (b.retrieveData("type") == "input") {
            b.onInputFocus(this);
            if (!a) {
                this.callEvent(b.retrieveData("input"), "focus")
            }
        } else {
            b.fire("onBoxFocus")
        }
        this.current = b;
        return this
    },blur: function(b) {
        if (!this.current) {
            return this
        }
        if (this.current.retrieveData("type") == "input") {
            var a = this.current.retrieveData("input");
            if (!b) {
                this.callEvent(a, "blur")
            }
            a.onInputBlur(this)
        } else {
            this.current.fire("onBoxBlur")
        }
        if (this.current.retrieveData("small") && !a.get("value") && this.options.get("hideempty")) {
            this.current.hide()
        }
        this.current.removeClassName(this.options.get("className") + "-" + this.current.retrieveData("type") + "-focus");
        this.current = false;
        return this
    },createBox: function(c, a) {
        var b = new Element("li", a).addClassName(this.options.get("className") + "-box").update(c.caption).cacheData("type", "box");
        return b
    },createInput: function(b) {
        var e = Object.extend(b, {type: "text",autocomplete: "off"});
        var a = new Element("li", {className: this.options.get("className") + "-input"});
        var d = new Element("input", b);
        d.observe("click", function(f) {
            f.stop()
        }).observe("focus", function(f) {
            if (!this.isSelfEvent("focus")) {
                this.focus(a, true)
            }
        }.bind(this)).observe("blur", function() {
            if (!this.isSelfEvent("blur")) {
                this.blur(true)
            }
        }.bind(this)).observe("keydown", function(f) {
            this.cacheData("lastvalue", this.value).cacheData("lastcaret", this.getCaretPosition())
        });
        var c = a.cacheData("type", "input").cacheData("input", d).insert(d);
        return c
    },callEvent: function(b, a) {
        this.events.set(a, b);
        b[a]()
    },isSelfEvent: function(a) {
        return (this.events.get(a)) ? !!this.events.unset(a) : false
    },makeResizable: function(a) {
        var b = a.retrieveData("input");
        b.cacheData("resizable", new ResizableTextbox(b, Object.extend(this.options.get("resizable"), {min: b.offsetWidth,max: (this.element.getWidth() ? this.element.getWidth() : 0)})));
        return this
    },checkInput: function() {
        var a = this.current.retrieveData("input");
        return (!a.retrieveData("lastvalue") || (a.getCaretPosition() === 0 && a.retrieveData("lastcaret") === 0))
    },move: function(b) {
        var a = this.current[(b == "left" ? "previous" : "next")]();
        if (a && (!this.current.retrieveData("input") || ((this.checkInput() || b == "right")))) {
            this.focus(a)
        }
        return this
    },moveDispose: function() {
        if (this.current.retrieveData("type") == "box") {
            this.dispose(this.current)
        }
        if (this.checkInput() && this.bits.keys().length && this.current.previous()) {
            return this.focus(this.current.previous())
        }
    }});
Element.addMethods({getCaretPosition: function() {
        if (this.createTextRange) {
            var a = document.selection.createRange().duplicate();
            a.moveEnd("character", this.value.length);
            if (a.text === "") {
                return this.value.length
            }
            return this.value.lastIndexOf(a.text)
        } else {
            return this.selectionStart
        }
    },cacheData: function(b, a, c) {
        if (Object.isUndefined(this[$(b).identify()]) || !Object.isHash(this[$(b).identify()])) {
            this[$(b).identify()] = $H()
        }
        this[$(b).identify()].set(a, c);
        return b
    },retrieveData: function(b, a) {
        return this[$(b).identify()].get(a)
    }});
function $pick() {
    for (var b = 0, a = arguments.length; b < a; b++) {
        if (!Object.isUndefined(arguments[b])) {
            return arguments[b]
        }
    }
    return null
}
var FacebookList = Class.create(TextboxList, {initialize: function($super, c, e, a, d) {
        $super(c, a);
        this.loptions = $H({autocomplete: {opacity: 1,maxresults: 10,minchars: 1}});
        this.id_base = $(c).identify() + "_" + this.options.get("className");
        this.data = [];
        this.data_searchable = [];
        this.autoholder = $(e).setOpacity(this.loptions.get("autocomplete").opacity);
        this.autoholder.observe("mouseover", function() {
            this.curOn = true
        }.bind(this)).observe("mouseout", function() {
            this.curOn = false
        }.bind(this));
        this.autoresults = this.autoholder.select("ul").first();
        var b = this.autoresults.select("li");
        b.each(function(f) {
            this.add({value: f.readAttribute("value"),caption: f.innerHTML})
        }, this);
        if (!Object.isUndefined(this.options.get("fetchFile"))) {
            new Ajax.Request(this.options.get("fetchFile"), {method: this.options.get("fetchMethod"),onSuccess: function(f) {
                    JSON.parse(f.responseText).each(function(g) {
                        this.autoFeed(g)
                    }.bind(this))
                }.bind(this)})
        }
    },autoShow: function(d) {
        this.autoholder.setStyle({display: "block"});
        this.autoholder.descendants().each(function(h) {
            h.hide()
        });
        if (!d || !d.strip() || (!d.length || d.length < this.loptions.get("autocomplete").minchars)) {
            this.autoholder.select(".default").first().setStyle({display: "block"});
            this.resultsshown = false
        } else {
            this.resultsshown = true;
            this.autoresults.setStyle({display: "block"}).update("");
            if (!this.options.get("regexSearch")) {
                var g = new Array();
                if (d) {
                    if (!this.options.get("caseSensitive")) {
                        d = d.toLowerCase()
                    }
                    var b = 0;
                    for (var c = 0, a = this.data_searchable.length; c < a; c++) {
                        if (this.data_searchable[c].indexOf(d) >= 0) {
                            g[b++] = this.data[c]
                        }
                    }
                }
            } else {
                if (this.options.get("wordMatch")) {
                    var f = new RegExp("(^|\\s)" + d, (!this.options.get("caseSensitive") ? "i" : ""))
                } else {
                    var f = new RegExp(d, (!this.options.get("caseSensitive") ? "i" : ""));
                    var g = this.data.filter(function(h) {
                        return h ? f.test(JSON.parse(h).caption) : false
                    })
                }
            }
            var e = 0;
            g = g.compact();
            g = g.sortBy(function(h) {
                h = JSON.parse(h);
                return h.value.startsWith(d)
            }).reverse();
            g.each(function(h, j) {
                e++;
                if (j >= (this.options.get("maxResults") ? this.options.get("maxResults") : this.loptions.get("autocomplete").maxresults)) {
                    return
                }
                var k = this;
                var i = new Element("li");
                i.observe("click", function(l) {
                    l.stop();
                    k.current_input = "";
                    k.autoAdd(this)
                }).observe("mouseover", function() {
                    k.autoFocus(this)
                }).update(this.autoHighlight(JSON.parse(h).caption, d));
                this.autoresults.insert(i);
                i.cacheData("result", JSON.parse(h));
                if (j == 0) {
                    this.autoFocus(i)
                }
            }, this)
        }
        if (e == 0) {
            this.autoHide()
        } else {
            if (e > this.options.get("results")) {
                this.autoresults.setStyle({height: (this.options.get("results") * 24) + "px"})
            } else {
                this.autoresults.setStyle({height: (e ? (e * 24) : 0) + "px"})
            }
        }
        return this
    },autoHighlight: function(b, a) {
        return b.gsub(new RegExp(a, "i"), function(c) {
            return "<em>" + c[0] + "</em>"
        })
    },autoHide: function() {
        this.resultsshown = false;
        this.autoholder.hide();
        return this
    },autoFocus: function(a) {
        if (!a) {
            return
        }
        if (this.autocurrent) {
            this.autocurrent.removeClassName("auto-focus")
        }
        this.autocurrent = a.addClassName("auto-focus");
        return this
    },autoMove: function(a) {
        if (!this.resultsshown) {
            return
        }
        this.autoFocus(this.autocurrent[(a == "up" ? "previous" : "next")]());
        this.autoresults.scrollTop = this.autocurrent.positionedOffset()[1] - this.autocurrent.getHeight();
        return this
    },autoFeed: function(c) {
        var b = this.options.get("caseSensitive"), a = JSON.stringify(c);
        if (this.data.indexOf(a) == -1) {
            this.data.push(a);
            this.data_searchable.push(b ? JSON.parse(JSON.stringify(c)).caption : JSON.parse(JSON.stringify(c)).caption.toLowerCase())
        }
        return this
    },autoAdd: function(b) {
        if (this.newvalue && this.options.get("newValues")) {
            this.add({caption: b.value,value: b.value,newValue: true});
            var a = b
        } else {
            if (!b || !b.retrieveData("result")) {
                return
            } else {
                this.add(b.retrieveData("result"));
                delete this.data[this.data.indexOf(JSON.stringify(b.retrieveData("result")))];
                var a = this.lastinput || this.current.retrieveData("input")
            }
        }
        this.autoHide();
        a.clear().focus();
        return this
    },createInput: function($super, c) {
        var a = $super(c);
        var b = a.retrieveData("input");
        b.observe("keydown", function(d) {
            this.dosearch = false;
            this.newvalue = false;
            switch (d.keyCode) {
                case Event.KEY_UP:
                    d.stop();
                    return this.autoMove("up");
                case Event.KEY_DOWN:
                    d.stop();
                    return this.autoMove("down");
                case Event.KEY_RETURN:
                case Event.KEY_TAB:
                    if (String("").valueOf() == String(this.current.retrieveData("input").getValue()).valueOf()) {
                        this.options.get("onEmptyInput")()
                    }
                    d.stop();
                    if (!this.autocurrent || !this.resultsshown) {
                        break
                    }
                    this.current_input = "";
                    this.autoAdd(this.autocurrent);
                    this.autocurrent = false;
                    this.autoenter = true;
                    setTimeout(function() {
                        this.focus(this.maininput)
                    }.bind(this), 100);
                    break;
                case Event.KEY_ESC:
                    this.autoHide();
                    if (this.current && this.current.retrieveData("input")) {
                        this.current.retrieveData("input").clear()
                    }
                    break;
                default:
                    this.dosearch = true
            }
        }.bind(this));
        b.observe("keyup", function(g) {
            var f = this.options.get("separator").code;
            var d = this.options.get("separator").value;
            switch (g.keyCode) {
                case f:
                    if (this.options.get("newValues")) {
                        new_value_el = this.current.retrieveData("input");
                        if (!new_value_el.value.endsWith("<")) {
                            keep_input = "";
                            if (new_value_el.value.indexOf(d) < (new_value_el.value.length - d.length)) {
                                separator_pos = new_value_el.value.indexOf(d);
                                keep_input = new_value_el.value.substr(separator_pos + 1);
                                new_value_el.value = new_value_el.value.substr(0, separator_pos).escapeHTML().strip()
                            } else {
                                new_value_el.value = new_value_el.value.gsub(d, "").escapeHTML().strip()
                            }
                            if (!this.options.get("spaceReplace").blank()) {
                                new_value_el.value.gsub(" ", this.options.get("spaceReplace"))
                            }
                            if (!new_value_el.value.blank()) {
                                g.stop();
                                this.newvalue = true;
                                this.current_input = keep_input.escapeHTML().strip();
                                this.autoAdd(new_value_el);
                                b.value = keep_input;
                                this.update()
                            }
                        }
                    }
                    break;
                case Event.KEY_UP:
                case Event.KEY_DOWN:
                case Event.KEY_RETURN:
                case Event.KEY_ESC:
                    break;
                default:
                    this.current_input = b.value.strip().escapeHTML();
                    this.update();
                    if (this.searchTimeout) {
                        clearTimeout(this.searchTimeout)
                    }
                    this.searchTimeout = setTimeout(function() {
                        var e = new RegExp("[({[^$*+?\\]})]", "g");
                        if (this.dosearch) {
                            this.autoShow(b.value.replace(e, "\\$1"))
                        }
                    }.bind(this), 250)
            }
        }.bind(this));
        b.observe(Prototype.Browser.IE ? "keydown" : "keypress", function(d) {
            if ((d.keyCode == Event.KEY_RETURN) && this.autoenter) {
                d.stop()
            }
            this.autoenter = false
        }.bind(this));
        return a
    },createBox: function($super, e, d) {
        var b = $super(e, d);
        b.observe("mouseover", function() {
            this.addClassName("bit-hover")
        }).observe("mouseout", function() {
            this.removeClassName("bit-hover")
        });
        var c = new Element("a", {href: "#","class": "closebutton"});
        c.observe("click", function(a) {
            a.stop();
            if (!this.current) {
                this.focus(this.maininput)
            }
            this.dispose(b)
        }.bind(this));
        b.insert(c).cacheData("text", JSON.stringify(e));
        return b
    }});
Element.addMethods({onBoxDispose: function(a, b) {
        a = JSON.parse(a.retrieveData("text"));
        if (!a.newValue) {
            b.autoFeed(a)
        }
    },onInputFocus: function(a, b) {
        b.autoShow()
    },onInputBlur: function(a, b) {
        b.lastinput = a;
        if (!b.curOn) {
            b.blurhide = b.autoHide.bind(b).delay(0.1)
        }
    },filter: function(c, b) {
        var d = [];
        for (var e = 0, a = this.length; e < a; e++) {
            if (c.call(b, this[e], e, this)) {
                d.push(this[e])
            }
        }
        return d
    }});
(function() {
    if (!APE.widget) {
        this.APE.widget = {}
    }
    var c = APE.dom.addClass, h = APE.dom.removeClass, b = APE.EventPublisher.add, e = APE.EventPublisher.remove, d = APE.dom.Event.preventDefault, f = APE.dom.Event.stopPropagation;
    APE.widget.ToggleButton = function(k) {
        this.buttonEl = APE.getElement(k);
        a.call(this)
    };
    function a() {
        b(this.buttonEl, "onclick", i, this);
        b(this.buttonEl, "onmouseover", j, this);
        b(this.buttonEl, "onmouseout", g, this)
    }
    function i(k) {
        this[this.isActive() ? "deactivate" : "activate"]();
        f(k)
    }
    function j() {
        c(this.buttonEl, "hover")
    }
    function g() {
        h(this.buttonEl, "hover")
    }
    APE.widget.ToggleButton.prototype = {onStateChange: function() {
        },enable: function() {
            this.buttonEl.disabled = false;
            h(this.buttonEl, "disabled")
        },disable: function() {
            this.deactivate();
            this.buttonEl.disabled = true;
            c(this.buttonEl, "disabled")
        },isDisabled: function() {
            return this.buttonEl.disabled
        },activate: function() {
            if (!this.isDisabled()) {
                this._active = true;
                c(this.buttonEl, "active");
                this.onStateChange()
            }
        },deactivate: function() {
            if (!this.isDisabled()) {
                this._active = false;
                h(this.buttonEl, "active");
                this.onStateChange()
            }
        },isActive: function() {
            return !!this._active
        },destroy: function() {
            e(this.buttonEl, "onclick", i, this);
            e(this.buttonEl, "onmouseover", j, this);
            e(this.buttonEl, "onmouseout", g, this);
            this.buttonEl = null
        },toElement: function() {
            return this.buttonEl
        }}
})();
(function() {
    if (!APE.widget.ToggleButton) {
        return
    }
    APE.widget.MenuButton = function(h, j, i) {
        this.menuEl = APE.getElement(j);
        this.config = {};
        i = i || {};
        for (var k in a) {
            this.config[k] = (k in i) ? i[k] : a[k]
        }
        this.labelEl = this.config.labelEl;
        g.call(this);
        return c.call(this, h)
    };
    var a = APE.widget.MenuButton.config = {menuOffsetX: 0,menuOffsetY: 0,toggleProperty: "visibility",toggleHideValue: "hidden",toggleShowValue: "visible",labelEl: null,onShow: function() {
        }};
    var c = APE.widget.ToggleButton, f = APE.dom.getOffsetCoords, e = APE.EventPublisher.add, b = APE.dom.contains, d = APE.dom.Event.getTarget;
    function g() {
        var h = this;
        e(document, "onclick", function(j) {
            var i = d(j);
            if (h._active && i !== h.menuEl && i !== h.buttonEl && !b(h.menuEl, i) && !b(h.buttonEl, i)) {
                h.deactivate()
            }
        })
    }
    APE.extend(APE.widget.MenuButton, APE.widget.ToggleButton, {showMenu: function() {
            var k = f(this.buttonEl);
            var i = document.viewport.getHeight();
            var h = this.buttonEl.viewportOffset().top + this.menuEl.getHeight();
            if (h >= i) {
                this.menuEl.style.top = (k.y - this.menuEl.offsetHeight + this.config.menuOffsetY) + "px"
            } else {
                var j = (k.y + this.buttonEl.offsetHeight + this.config.menuOffsetY);
                this.menuEl.style.top = j + "px"
            }
            this.menuEl.style.left = (k.x + this.config.menuOffsetX) + "px";
            this.menuEl.style[this.config.toggleProperty] = this.config.toggleShowValue;
            this.config.onShow();
            document.fire("menu:show", {instance: this})
        },hideMenu: function() {
            this.menuEl.style[this.config.toggleProperty] = this.config.toggleHideValue
        },activate: function() {
            c.prototype.activate.call(this);
            if (!this.isDisabled()) {
                this.showMenu()
            }
        },deactivate: function() {
            c.prototype.deactivate.call(this);
            this.hideMenu()
        },disable: function() {
            c.prototype.disable.call(this);
            if (this.labelEl) {
                APE.dom.addClass(this.labelEl, "disabled")
            }
            this.hideMenu()
        },enable: function() {
            c.prototype.enable.call(this);
            if (this.labelEl) {
                APE.dom.removeClass(this.labelEl, "disabled")
            }
        },hide: function() {
            if (this.labelEl) {
                this.labelEl.style.display = "none"
            }
            this.buttonEl.style.display = "none"
        },show: function() {
            if (this.labelEl) {
                this.labelEl.style.display = ""
            }
            this.buttonEl.style.display = ""
        }})
})();
(function() {
    if (!APE || (APE && !APE.widget)) {
        return
    }
    var f = APE.EventPublisher.add, h = APE.dom.Event.preventDefault, c = APE.dom.Event.getTarget, g = APE.dom.addClass, j = APE.dom.removeClass;
    var d = ["000", "300", "600", "900", "C00", "F00", "003", "303", "603", "903", "C03", "F03", "006", "306", "606", "906", "C06", "F06", "009", "309", "609", "909", "C09", "F09", "00C", "30C", "60C", "90C", "C0C", "F0C", "00F", "30F", "60F", "90F", "C0F", "F0F", "030", "330", "630", "930", "C30", "F30", "033", "333", "633", "933", "C33", "F33", "036", "336", "636", "936", "C36", "F36", "039", "339", "639", "939", "C39", "F39", "03C", "33C", "63C", "93C", "C3C", "F3C", "03F", "33F", "63F", "93F", "C3F", "F3F", "060", "360", "660", "960", "C60", "F60", "063", "363", "663", "963", "C63", "F63", "066", "366", "666", "966", "C66", "F66", "069", "369", "669", "969", "C69", "F69", "06C", "36C", "66C", "96C", "C6C", "F6C", "06F", "36F", "66F", "96F", "C6F", "F6F", "090", "390", "690", "990", "C90", "F90", "093", "393", "693", "993", "C93", "F93", "096", "396", "696", "996", "C96", "F96", "099", "399", "699", "999", "C99", "F99", "09C", "39C", "69C", "99C", "C9C", "F9C", "09F", "39F", "69F", "99F", "C9F", "F9F", "0C0", "3C0", "6C0", "9C0", "CC0", "FC0", "0C3", "3C3", "6C3", "9C3", "CC3", "FC3", "0C6", "3C6", "6C6", "9C6", "CC6", "FC6", "0C9", "3C9", "6C9", "9C9", "CC9", "FC9", "0CC", "3CC", "6CC", "9CC", "CCC", "FCC", "0CF", "3CF", "6CF", "9CF", "CCF", "FCF", "0F0", "3F0", "6F0", "9F0", "CF0", "FF0", "0F3", "3F3", "6F3", "9F3", "CF3", "FF3", "0F6", "3F6", "6F6", "9F6", "CF6", "FF6", "0F9", "3F9", "6F9", "9F9", "CF9", "FF9", "0FC", "3FC", "6FC", "9FC", "CFC", "FFC", "0FF", "3FF", "6FF", "9FF", "CFF", "FFF", "transparent"];
    d = d.eachSlice(18);
    function e() {
        b.call(this);
        a.call(this)
    }
    e.config = {cellTitle: "Color hex code is #{hexCode}"};
    var i = /^([0-9a-f]{3}|[0-9a-f]{6})$/i;
    function b() {
        this.containerEl = document.createElement("div");
        this.containerEl.className = "palette-container";
        var m = document.createElement("table");
        var n = document.createElement("thead");
        var l = this.tbodyEl = document.createElement("tbody");
        m.appendChild(n);
        k(l);
        m.appendChild(l);
        this.containerEl.appendChild(m)
    }
    function k(t) {
        for (var o = 0, s = d.length; o < s; o++) {
            var m = document.createElement("tr");
            for (var n = 0, q = d[o].length; n < q; n++) {
                var l = document.createElement("td");
                var r = document.createElement("a");
                var p = d[o][n];
                r.href = "#";
                r.style.backgroundColor = i.test(p) ? ("#" + p) : p;
                if (p === "transparent") {
                    r.className = "transparent"
                }
                r.title = e.config.cellTitle.interpolate({hexCode: p});
                l.appendChild(r);
                m.appendChild(l)
            }
            t.appendChild(m)
        }
    }
    function a() {
        var m = this;
        var l;
        f(this.containerEl, "onclick", function(o) {
            o = o || window.event;
            var n = c(o);
            h(o);
            if (n.tagName.toLowerCase() !== "a") {
                return
            }
            if (l) {
                j(l, "selected")
            }
            g(n, "selected");
            l = n;
            m.onSelect(n)
        })
    }
    APE.mixin(e.prototype, {appendTo: function(l) {
            l.appendChild(this.containerEl)
        },getContainerEl: function() {
            return this.containerEl
        },onSelect: function() {
        },loadPalette: function(m) {
            d = m.eachSlice(18);
            var l = this.tbodyEl;
            while (l.firstChild) {
                l.removeChild(l.firstChild)
            }
            k(this.tbodyEl)
        }});
    APE.widget.PalettePicker = e
})();
(function() {
    APE.namespace("APE.widget");
    function c() {
    }
    function l(m) {
        return m.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/"/g, "&quot;")
    }
    function a(m) {
        return typeof m == "string" ? document.getElementById(m) : m
    }
    function e(n, m) {
        m.parentNode.insertBefore(n, m)
    }
    function d(n, m) {
        m.parentNode.insertBefore(n, m.nextSibling)
    }
    var g = APE.dom.addClass, k = APE.dom.removeClass, i = APE.dom.Event.addCallback, b = APE.dom.Event.getTarget, f = APE.dom.Event.preventDefault, h = APE.dom.Event.stopPropagation;
    function j(n, m) {
        m = m || {};
        if (m.onSuccess) {
            this.onSuccess = m.onSuccess
        }
        if (m.onCancel) {
            this.onCancel = m.onCancel
        }
        this.defaultValue = j.config.defaultValue;
        this.inputEl = a(n);
        this._init()
    }
    j.config = {defaultValue: "",cancelText: "Cancel",okText: "Ok",replacementTitleText: "Click to edit"};
    j.prototype = {onCancel: c,onSuccess: c,_init: function() {
            this._buildElements();
            this._initBehavior()
        },_buildElements: function() {
            var o = this.replacementEl = document.createElement("div");
            o.className = "ipe-replacement";
            o.title = j.config.replacementTitleText;
            o.tabIndex = 0;
            o.innerHTML = l(this.inputEl.value);
            this.inputEl.style.display = "none";
            var n = this.okEl = document.createElement("input");
            n.type = "button";
            n.value = j.config.okText;
            n.className = "ipe-ok";
            n.style.display = "none";
            var m = this.cancelEl = document.createElement("a");
            m.href = "#";
            m.title = j.config.cancelText;
            m.innerHTML = j.config.cancelText;
            m.className = "ipe-cancel";
            m.style.display = "none";
            e(o, this.inputEl);
            d(this.cancelEl, this.inputEl);
            d(this.okEl, this.inputEl)
        },_successHandler: function() {
            var m = this.inputEl.value;
            this.replacementEl.firstChild.nodeValue = m || this.defaultValue;
            this.hideControls();
            this.onSuccess(m);
            return false
        },_cancelHandler: function() {
            this.hideControls();
            this.onCancel();
            return false
        },_initBehavior: function() {
            var m = this;
            i(this.replacementEl, "mouseover", function(n) {
                g(b(n), "hover")
            });
            i(this.replacementEl, "mouseout", function(n) {
                k(b(n), "hover")
            });
            i(this.replacementEl, "click", function() {
                m.showControls()
            });
            i(this.replacementEl, "keyup", function(n) {
                if (n.keyCode === 13 || n.keyCode === 32) {
                    m.showControls()
                }
            });
            i(this.okEl, "click", function(n) {
                f(n);
                m._successHandler()
            });
            i(this.cancelEl, "click", function(n) {
                f(n);
                m._cancelHandler()
            });
            i(this.inputEl, "keyup", function(n) {
                if (!m.controlsHidden) {
                    switch (n.keyCode) {
                        case 13:
                            m._successHandler();
                            break;
                        case 27:
                            m._cancelHandler();
                            break
                    }
                }
            });
            i(this.inputEl, "keydown", function(n) {
                if (n.keyCode === 13) {
                    f(n)
                }
            })
        },showControls: function() {
            this.replacementEl.style.display = "none";
            this.inputEl.style.display = "";
            this.okEl.style.display = "";
            this.cancelEl.style.display = "";
            this.inputEl.focus();
            this.inputEl.select();
            this.controlsHidden = false
        },hideControls: function() {
            this.replacementEl.style.display = "";
            this.inputEl.style.display = "none";
            this.okEl.style.display = "none";
            this.cancelEl.style.display = "none";
            this.controlsHidden = true
        }};
    APE.widget.InPlaceEditor = j
})();
(function() {
    var c = 20.5, a = 2.5, d = "rgb(0,200,255)", b = "rgba(0,0,0,0.5)";
    this.dashboardMixins = this.dashboardMixins || {};
    function e(h, j, g) {
        var i = h.fillStyle;
        h.fillStyle = b;
        h.fillRect(0, 0, c + a, g);
        h.fillRect(c + a, 0, j, c + a);
        h.fillRect(j - c - a, c + a, c + a, g);
        h.fillRect(c + a, g - c - a, j - ((c + a) * 2), c + a);
        h.fillStyle = i
    }
    function f(h, i, g) {
        h.moveTo(c, 0);
        h.lineTo(c, g);
        h.moveTo(0, g - c);
        h.lineTo(i, g - c);
        h.moveTo(i - c, g);
        h.lineTo(i - c, 0);
        h.moveTo(i, c);
        h.lineTo(0, c);
        h.moveTo(0, 0)
    }
    dashboardMixins.guidelines = {drawGuidelines: function() {
            var i = this.canvas.getWidth(), g = this.canvas.getHeight(), h = this.canvas.contextTop;
            h.strokeStyle = d;
            h.beginPath();
            e(h, i, g);
            f(h, i, g);
            h.stroke()
        }}
})();
function createCenteringGuidelines(c) {
    var s = c.getWidth(), b = c.getHeight(), n = s / 2, w = b / 2, q = {}, r = {}, k = 4, m = "rgba(255,0,241,0.5)", j = 1, l = c.contextTop;
    for (var o = n - k, p = n + k; o <= p; o++) {
        q[o] = true
    }
    for (var o = w - k, p = w + k; o <= p; o++) {
        r[o] = true
    }
    function a() {
        e(n + 0.5, 0, n + 0.5, b)
    }
    function f() {
        e(0, w + 0.5, s, w + 0.5)
    }
    function e(x, z, i, y) {
        l.save();
        l.strokeStyle = m;
        l.lineWidth = j;
        l.beginPath();
        l.moveTo(x, z);
        l.lineTo(i, y);
        l.stroke();
        l.restore()
    }
    var v = [], h, u;
    function g(i) {
        object = i.memo.target;
        h = object.get("left") in q, u = object.get("top") in r;
        if (u) {
            object.set("top", w)
        }
        if (h) {
            object.set("left", n)
        }
    }
    function t() {
        if (h) {
            a()
        }
        if (u) {
            f()
        }
    }
    function d() {
        h = u = null;
        c.renderAll()
    }
    return {enable: function() {
            c.observe("object:moving", g);
            c.observe("after:render", t);
            c.observe("mouse:up", d)
        },disable: function() {
            c.observe("object:moving", g);
            c.observe("after:render", t);
            c.observe("mouse:up", d)
        }}
}
;
function createAligningGuidelines(c) {
    var p = c.getContext(), f = c.getHeight(), b = 5, a = 4, j = 1, o = "rgb(0,255,0)";
    function h(q) {
        l(q.x + 0.5, q.y1 > q.y2 ? q.y2 : q.y1, q.x + 0.5, q.y2 > q.y1 ? q.y2 : q.y1)
    }
    function d(q) {
        l(q.x1 > q.x2 ? q.x2 : q.x1, q.y + 0.5, q.x2 > q.x1 ? q.x2 : q.x1, q.y + 0.5)
    }
    function l(r, t, q, s) {
        p.save();
        p.lineWidth = j;
        p.strokeStyle = o;
        p.beginPath();
        p.moveTo(r, t);
        p.lineTo(q, s);
        p.stroke();
        p.restore()
    }
    function e(s, r) {
        s = Math.round(s);
        r = Math.round(r);
        for (var t = s - a, q = s + a; t <= q; t++) {
            if (t === r) {
                return true
            }
        }
        return false
    }
    var n = [], i = [];
    function k(w) {
        var r = w.memo.target, t = c.getObjects(), v = r.get("left"), C = r.get("top"), x = r.getHeight(), A = r.getWidth(), y = true;
        for (var u = t.length; u--; ) {
            if (t[u] === r) {
                continue
            }
            var z = t[u].get("left"), B = t[u].get("top"), q = t[u].getHeight(), s = t[u].getWidth();
            if (e(z, v)) {
                y = false;
                n.push({x: z,y1: (B < C) ? (B - q / 2 - b) : (B + q / 2 + b),y2: (C > B) ? (C + x / 2 + b) : (C - x / 2 - b)});
                r.set("left", z)
            }
            if (e(z - s / 2, v - A / 2)) {
                y = false;
                n.push({x: z - s / 2,y1: (B < C) ? (B - q / 2 - b) : (B + q / 2 + b),y2: (C > B) ? (C + x / 2 + b) : (C - x / 2 - b)});
                r.set("left", z - s / 2 + A / 2)
            }
            if (e(z + s / 2, v + A / 2)) {
                y = false;
                n.push({x: z + s / 2,y1: (B < C) ? (B - q / 2 - b) : (B + q / 2 + b),y2: (C > B) ? (C + x / 2 + b) : (C - x / 2 - b)});
                r.set("left", z + s / 2 - A / 2)
            }
            if (e(B, C)) {
                y = false;
                i.push({y: B,x1: (z < v) ? (z - s / 2 - b) : (z + s / 2 + b),x2: (v > z) ? (v + A / 2 + b) : (v - A / 2 - b)});
                r.set("top", B)
            }
            if (e(B - q / 2, C - x / 2)) {
                y = false;
                i.push({y: B - q / 2,x1: (z < v) ? (z - s / 2 - b) : (z + s / 2 + b),x2: (v > z) ? (v + A / 2 + b) : (v - A / 2 - b)});
                r.set("top", B - q / 2 + x / 2)
            }
            if (e(B + q / 2, C + x / 2)) {
                y = false;
                i.push({y: B + q / 2,x1: (z < v) ? (z - s / 2 - b) : (z + s / 2 + b),x2: (v > z) ? (v + A / 2 + b) : (v - A / 2 - b)});
                r.set("top", B + q / 2 - x / 2)
            }
        }
        if (y) {
            n.length = i.length = 0
        }
    }
    function m() {
        for (var q = n.length; q--; ) {
            h(n[q])
        }
        for (var q = i.length; q--; ) {
            d(i[q])
        }
    }
    function g() {
        n.length = i.length = 0;
        c.renderAll()
    }
    return {enable: function() {
            c.observe("object:moving", k);
            c.observe("after:render", m);
            c.observe("mouse:up", g)
        },disable: function() {
            c.stopObserving("object:moving", k);
            c.stopObserving("after:render", m);
            c.stopObserving("mouse:up", g)
        }}
}
;
(function() {
    var e = this, d = e.window, a = d.document, b = true;
    var c = Class.create(dashboardMixins.guidelines, {initialize: function() {
            this.initPrimaryUI();
            this.initSecondaryUI()
        },initPrimaryUI: function() {
            this.fontDefinitions = {};
            this.shapesCache = {};
            this.canvas = e.__canvas = new fabric.Canvas("canvas");
            this.canvas.includeDefaultValues = false;
            var f = $w("initUIElements setCanvasBgTransparent positionSidebar initProductMeta          setDefaultSide setInitialColor displayJSDependentContent          initColorMap initCanvasBgColorPicker setInitialBgColor          initFreeDrawing initSurfaces setBgColorpickerColor initInPlaceEditor          initTextPlaceholder initTextAddition initUIControls positionImageUploadForm          initDesignTitleField initDesignTitleField initActiveColorBtn initTextBgColorBtn          initActiveOpacityBtn initActiveFontsizeBtn initPriceController initBusinessCardLogic          initPuzzleLogic initMousePadLogic initPoloLogic initPillowLogic initContextMenu          initMoreButton initDeleteButton          initActiveControlObserver initClearCanvas initUnselectableElements          initDesignsThumbnails initColorSelection initOpacitySlider initFontsizeSlider          initFontOptionsMenu initLineHeightMenu initTextAlignMenu initSaveDesign          initAddToCart initSellThisItem initRasterizeDesign initSidebarThumbDragger          renderAppButtons initChooseFontMenu initOffsetRecalcOnLayoutChange initClipping");
            f.each(function(g) {
                try {
                    this[g]()
                } catch (h) {
                    throw h
                }
            }, this)
        },initSecondaryUI: function() {
            var f = $w("initHistory initActiveColorPicker initTextBgColorPicker initRemoveBg initSVGCache         initImageLoading initOnUnloadCleanup initOnBeforeUnloadWarning initUploader         initFocusBlurObserver initKeyObserver initImageRemoval initCoordinatesUpdater         initAligningCenteringGuidelines initScaledImagesWarning preventCanvasTextualSelection         initObjectDeselection initMenuHiding initMugLogic");
            f.forEach(function(g) {
                this[g]()
            }, this);
            this.canvas.calcOffset()
        },initObjectDeselection: function() {
            a.observe("click", (function(f) {
                var g = f.target.id;
                if (g === "canvas-bg" || g === "wrap" || g === "canvas-wrapper") {
                    this.deactivateAll()
                }
            }).bind(this))
        },initRealisticPreview: function() {
            var f = $("realistic-preview");
            if (f) {
                f.observe("click", this.initRealisticPreviewModal.bind(this));
                f.show()
            }
            this.realisticPreviewWrapper = $("realistic-preview-wrapper");
            this.modalShim = $("modal-shim");
            if (!this.realisticPreviewWrapper) {
                return
            }
            this.initRealisticPreviewObservers();
            if (a.cookie.indexOf("realistic_preview") !== -1) {
                a.cookie = "realistic_preview=;expires=Thu, 01 Jan 1970 00:00:00 GMT;path=/";
                this.initRealisticPreviewModal(null, true)
            }
        },closeRealisticPreviewModal: function(f) {
            if (this.realisticPreviewWrapper.visible()) {
                f && f.stop();
                this.modalShim.hide();
                this.realisticPreviewWrapper.hide()
            }
        },initRealisticPreviewObservers: function() {
            this.realisticPreviewWrapper.down(".close-trigger").observe("click", this.closeRealisticPreviewModal.bind(this));
            this.modalShim.observe("click", this.closeRealisticPreviewModal.bind(this));
            a.observe("keydown", (function(f) {
                if (f.keyCode === Event.KEY_ESC) {
                    this.closeRealisticPreviewModal()
                }
            }).bind(this))
        },initRealisticPreviewModal: function(h, g) {
            h && h.stop();
            if (this.shouldSubmitForm) {
                var f = new Date();
                f.setTime(f.getTime() + (60 * 60 * 1000));
                a.cookie = "realistic_preview=1;expires=" + f + ";path=/;";
                this.saveDesignViaForm();
                return
            }
            c.RealisticPoller.init(this, g);
            this.modalShim.show();
            this.realisticPreviewWrapper.show()
        },deactivateAll: function() {
            this.canvas.deactivateAll().renderAll();
            this.textPopup.hide()
        },initProductMeta: function() {
            this.productMeta = JSON.parse($("product-meta").value)
        },positionSidebar: function() {
            $("canvas-wrapper").insert({after: this.sidebarEl})
        },setInitialBgColor: function() {
            this.setBgColor(this.INITIAL_BG_COLOR)
        },initClipping: function() {
            var f = this.getCurrentSideMetadata().mask;
            if (f) {
                this.canvas.clipTo = new Function("ctx", f)
            }
        },initPriceController: function() {
            this.priceController = new c.PriceController(this)
        },initOffsetRecalcOnLayoutChange: function() {
            var f = this;
            a.observe("layout:changed", function() {
                f.canvas.calcOffset()
            })
        },preventCanvasTextualSelection: function() {
            this.canvas.getElement().onselectstart = fabric.util.falseFunction
        },setCanvasBgTransparent: function() {
            try {
                this.canvas.getElement().style.backgroundColor = "rgba(0,0,0,0)"
            } catch (f) {
                this.canvas.getElement().style.backgroundColor = "transparent"
            }
        },setBgColorpickerColor: function() {
            setTimeout((function() {
                var f = $("cp2_Preview"), g = !!f;
                if (g) {
                    f.style.backgroundColor = this.canvas.backgroundColor === "rgba(0, 0, 0, 0)" ? "" : this.canvas.backgroundColor
                }
            }).bind(this), 50)
        },initCoordinatesUpdater: function() {
            var k = this, h, i = $("left-coordinate").firstChild, f = $("top-coordinate").firstChild, j = $("width").firstChild, g = $("height").firstChild;
            this.activeObjectProps = {init: function() {
                    k.canvas.observe("object:moving", function(l) {
                        h = l.memo.target;
                        i.data = ~~h.left;
                        f.data = ~~h.top
                    });
                    k.canvas.observe("object:scaling", function(l) {
                        h = l.memo.target;
                        j.data = ~~h.getWidth();
                        g.data = ~~h.getHeight()
                    })
                },set: function(o, n, m, l) {
                    i.data = typeof o == "number" ? o | 0 : o || "";
                    f.data = typeof n == "number" ? n | 0 : n || "";
                    j.data = typeof m == "number" ? m | 0 : m || "";
                    g.data = typeof l == "number" ? l | 0 : l || ""
                },update: function() {
                    var l = k.getActive();
                    if (l) {
                        this.set(l.get("left"), l.get("top"), l.getWidth(), l.getHeight())
                    }
                }};
            this.activeObjectProps.init()
        },initColorMap: function() {
            this.colorMap = JSON.parse($("color-hex-map").value)
        },initImageRemoval: function() {
            function g(i) {
                return i.replace(/\?\d*$/, "")
            }
            var h = this;
            var f = $$(".add-image")[0];
            if (!f) {
                return
            }
            f.observe("click", function(n, k) {
                if ((k = n.findElement("li .remove"))) {
                    n.stop();
                    var m = k.up("li").down(".shape-thumb"), j = m.id, i = g(m.href);
                    if (!j) {
                        return
                    }
                    j = j.replace(/^image-/, "");
                    new Ajax.Request("/images/" + j + ".json", {method: "delete"});
                    Element.fade(k.up("li"), {duration: 0.5});
                    var l = h.canvas.getObjects().findAll(function(o) {
                        if (o.type === "image") {
                            return g(o.getSrc()) === i
                        }
                    });
                    l.each(function(o) {
                        h.removeObject(o, b)
                    })
                }
            })
        },isBusinessCard: function() {
            return /business_card$/.test(this.productMeta.product)
        },isPuzzle: function() {
            return (this.productMeta.product === "puzzle_a3" || this.productMeta.product === "puzzle_a4")
        },initBusinessCardLogic: function() {
            var f = this;
            this.shouldDrawGuidelines = true;
            if (!this.isBusinessCard()) {
                return
            }
            this.guidelinesHelp = $("guidelines-help");
            this.guidelinesHelp.show();
            this.initGuidelinesToggler();
            this.canvas.observe("after:render", function() {
                if (f.shouldDrawGuidelines) {
                    f.drawGuidelines()
                }
            });
            this.initBgColorBtn()
        },initPuzzleLogic: function() {
            if (!this.isPuzzle()) {
                return
            }
            var f = this.getCurrentSideMetadata();
            this.canvas.setOverlayImage(f.layers.overlay.interpolate({color: this.getCurrentColor()}), this.canvas.renderAll.bind(this.canvas));
            this.canvasContainerEl.style.backgroundColor = "rgb(211,221,218)";
            $("canvas-bg").style.visibility = "hidden"
        },initGuidelinesToggler: function() {
            var h = this;
            var f = $("toggle-guidelines-container");
            if (f) {
                f.show();
                var g = $("toggle-guidelines");
                if (g) {
                    g.observe("click", function() {
                        h.shouldDrawGuidelines = this.checked;
                        h.canvas.renderAll();
                        h.guidelinesHelp[h.shouldDrawGuidelines ? "show" : "hide"]()
                    })
                }
            }
        },initMousePadLogic: function() {
            if (this.productMeta.product === "mouse_pad") {
                this.initBgColorBtn()
            }
        },initMugLogic: function() {
            if (this.productMeta.product === "mug") {
                this.initRealisticPreview()
            }
        },initPillowLogic: function() {
            if (this.productMeta.product === "pillow") {
                this.initFrontColoredOverlay()
            }
        },initPoloLogic: function() {
            if (this.productMeta.product === "polo_shirt") {
                this.initFrontColoredOverlay()
            }
        },initFrontColoredOverlay: function() {
            var f = (function(g) {
                var h = this.getCurrentSideMetadata();
                this.canvas.setOverlayImage(h.layers.overlay.interpolate({color: g || this.getCurrentColor()}), this.canvas.renderAll.bind(this.canvas))
            }).bind(this);
            f();
            a.observe("color:changed", (function(g) {
                f(this.normalizeBgColor(g.memo.color))
            }).bind(this));
            a.observe("surface:changed", (function(h) {
                var g = this.getCurrentSideMetadata().side;
                if (g === "back") {
                    this.canvas.overlayImage = "";
                    this.canvas.renderAll()
                } else {
                    f()
                }
            }).bind(this))
        },initScaledImagesWarning: function() {
            this.extendForScaledImagesWarning();
            var g = this;
            function f() {
                setTimeout(function() {
                    g.checkScaledUpImages()
                }, 50)
            }
            a.observe("content:changed", f).observe("design:loaded", f)
        },extendForScaledImagesWarning: function() {
            fabric.Image.prototype.toObject = (function(f) {
                return function() {
                    return fabric.util.object.extend(f.call(this), {originalWidth: this.get("originalWidth"),originalHeight: this.get("originalHeight")})
                }
            })(fabric.Image.prototype.toObject);
            fabric.Image.prototype._initConfig = (function(f) {
                return function(g) {
                    g = g || {};
                    f.call(this, g);
                    this.set("originalWidth", g.originalWidth);
                    this.set("originalHeight", g.originalHeight)
                }
            })(fabric.Image.prototype._initConfig)
        },checkScaledUpImages: function() {
            var f = $("scaled-images-warning");
            if (this.hasScaledUpImages()) {
                if (f.visible()) {
                    return
                }
                f.show();
                emile(f, "background-color: rgba(255, 0, 0, 0.9)", {duration: 50,after: function() {
                        emile(f, "background-color: rgba(255, 200, 200, 0.9)")
                    }})
            } else {
                f.hide()
            }
        },hasScaledUpImages: function() {
            var g = this.getCurrentSideMetadata().dpi;
            for (var f = this.canvas.getObjects(), h = f.length; h--; ) {
                if (this.isBelowMinDPI(f[h], g)) {
                    return true
                }
            }
            return false
        },isBelowMinDPI: function(g, f) {
            var i = 150, h = this.getHeightForCurrentSideDPI(g, i, f), j = this.getWidthForCurrentSideDPI(g, i, f);
            return (g.getHeight() > h || g.getWidth() > j)
        },initBgColorBtn: function() {
            if (!$("bg-color-btn")) {
                return
            }
            $("bg-color-btn").show();
            $$('[for="bg-color-btn"]')[0].show();
            this.bgColorPicker = new Refresh.Web.ColorPicker("cp2", {startMode: "h",clientFilesPath: this.COLORPICKER_IMAGES_PATH,controlsOn: false});
            var j = this;
            this.bgColorBtn = new APE.widget.MenuButton("bg-color-btn", "cp2_Container", {labelEl: $$('[for="bg-color-btn"]')[0],onShow: function() {
                    j.bgColorPicker.reflow()
                }});
            var f = this.bgColorBtn.toElement(), i = $(f).cumulativeOffset(), h = i.left, g = i.top + f.offsetHeight;
            this.bgColorPickerContainer.setStyle({left: h + "px",top: g + "px"});
            this.bgColorPicker.setValueFromHex(this.INITIAL_BG_COLOR || "FFFFFF")
        },initChooseFontMenu: function() {
            if (!this.fontSelector) {
                return
            }
            this.chooseFontMenu = c.ChooseFontMenu.init(this.fontSelector, {onFontsFetched: this.canvas.renderAll.bind(this.canvas),onMenuItemChosen: this.onChooseFontMenuItemChosen.bind(this)});
            this.chooseFontBtn = c.ChooseFontBtn.init(this.chooseFontMenu)
        },onChooseFontMenuItemChosen: function(l, h) {
            var i = this.canvas.getActiveObject(), j = this.canvas.getActiveGroup(), f = i || j;
            var k = (i && i.isType("text")) || (j && j.objects.all(function(m) {
                return m.type === "text"
            }));
            if (k) {
                f.saveState();
                f.set("fontFamily", l);
                var g = new fabric.TransformCommand(f, {stateProperties: ["fontFamily"]});
                this.history.add(g)
            }
            this.canvas.renderAll();
            this.updateChooseFontBtn(h.innerHTML);
            this.chooseFontBtn.deactivate()
        },initImageLoading: function() {
            c.ScrollingImageLoader.init($$(".add-shape ul")[0])
        },initSVGCache: function() {
            if (!c.SVGCache) {
                c.SVGCache = {get: Prototype.emptyFunction,set: function(f, g, h) {
                        h()
                    },clear: Prototype.emptyFunction,has: function(f) {
                        f(true)
                    }}
            }
            this.canvas.cache = c.SVGCache
        },initUIElements: function() {
            this.objectButtons = [];
            this.appButtons = [];
            this.saveProjectForm = $("product-form");
            this.canvasWrapperEl = $("canvas-wrapper");
            this.canvasContainerEl = $$(".canvas-container")[0];
            this.sidebarEl = $$(".top-nav-panels")[0];
            this.controlsWrapper = $("controls-bd");
            this.canvasBgPreviewEl = $("canvas-color-preview");
            this.activeColorPickerContainer = $("cp1_Container");
            this.bgColorPickerContainer = $("cp2_Container");
            this.activeOpacityLabel = $("active-opacity-value");
            this.activeFontsizeLabel = $("active-fontsize-value");
            this.undoBtn = $("undo");
            this.redoBtn = $("redo");
            this.statusMessage = $("status-message");
            this.newImageForm = $("new_image");
            this.addToCartField = $("add_to_cart");
            this.sellThisItemField = $("sell_this_item");
            this.sellThisItemBtn = $("sell-this-item-btn");
            this.fontSelector = $("font-selector");
            this.imagesThumbnailsEl = $$(".thumbnails-images")[0];
            this.spinnerEl = $("spinner")
        },getCurrentColor: function() {
            return this.colorHiddenFieldEl && this.colorHiddenFieldEl.value
        },setInitialColor: function() {
            this.colorHiddenFieldEl = $("item-default-color");
            if (this.colorHiddenFieldEl && this.colorHiddenFieldEl.value) {
                this.INITIAL_BG_COLOR = this.colorHiddenFieldEl.value
            } else {
                if (this.productMeta.default_color) {
                    this.INITIAL_BG_COLOR = this.productMeta.default_color
                }
            }
            if (this.productMeta.colors.indexOf(this.INITIAL_BG_COLOR) === -1) {
                this.INITIAL_BG_COLOR = this.productMeta.colors.length > 0 ? this.productMeta.colors[0] : ""
            }
        },displayJSDependentContent: function() {
            $("loading-indicator").style.display = "none";
            $$(".js-content").each(function(f) {
                f.style.visibility = "visible"
            })
        },initRasterizeDesign: function() {
            var f = this;
            this.rasterizeDesignBtn = $("rasterize-design");
            if (this.rasterizeDesignBtn) {
                this.rasterizeDesignBtn.observe("click", function() {
                    var h = f.canvas.CANVAS_PRINT_WIDTH / f.canvas.CANVAS_WIDTH, g = f.canvas.toDataURLWithMultiplier("png", h);
                    if (g) {
                        d.open(g)
                    }
                })
            }
        },initTextPlaceholder: function() {
            var f = $("text-placeholder");
            if (f) {
                this.textPlaceholder = new c.TextPlaceholder(f);
                this.textPlaceholder.observe("value:changed", this.onPlaceholderValueChanged.bind(this));
                this.textPlaceholder.observe("value:removed", this.onPlaceholderValueRemoved.bind(this))
            }
        },initTextAddition: function() {
            this.textPopup = new c.TextPopup("text-controls");
            var f = $("add-text-btn");
            if (f) {
                f.onclick = (function() {
                    this.addTextObject("Впишите текст...")
                }).bind(this)
            }
        },onPlaceholderValueChanged: function(i) {
            var j = i.memo.value, g = this.canvas.getActiveObject();
            this.textPlaceholder.element.value = j;
            if (g && g.isType("text")) {
                g.saveState().set("text", j).setCoords();
                this.history.add(new fabric.TransformCommand(g))
            } else {
                if (j !== "") {
                    var k = this.fontSelector, h = $A(k.options).find(function(l, m) {
                        return m === k.selectedIndex
                    }), f = this.addTextObject(j, {fontFamily: h.innerHTML.replace(/ /g, "_")});
                    this.canvas.setActiveObject(f)
                }
            }
            this.canvas.renderAll().renderAll()
        },onPlaceholderValueRemoved: function() {
            var f = this.canvas.getActiveObject();
            if (f && f.isType("text")) {
                this.canvas.remove(f)
            }
        },addTextObject: function(i, h) {
            var f = new fabric.Text(i, Object.extend({fill: this.INITIAL_TEXT_COLOR_VALUE,}, h || {}));
            f.path = this.FONT_PATH_TPL.interpolate(f);
            f.scale(0.8);
            var g = this.canvas.getCenter(), j = 100;
            f.set("left", g.left);
            f.set("top", g.top + fabric.util.getRandomInt(-j, j));
            this.addObject(f);
            this.canvas.setActiveObject(f);
            return f
        },replaceUrlsWithHashes: function(f) {
            return f.gsub(/http:\/\/[^"\\]+/, function(h) {
                if (h && h[0]) {
                    var i = $$('.url[value^="' + h[0] + '"]')[0];
                    if (i) {
                        var g = i.up().down(".hash");
                        if (g) {
                            return "###" + g.value + "###"
                        }
                    } else {
                        return h[0]
                    }
                }
            })
        },initSidebarThumbDragger: function() {
            if (this.sidebarEl) {
                c.ThumbDragger.initialize(this)
            }
        },initMoreButton: function() {
            var f = $("menu-btn"), g = this;
            if (f) {
                this.menuBtn = new APE.widget.MenuButton(f, this.contextMenu.container, {toggleProperty: "display",toggleShowValue: "",toggleHideValue: "none",onShow: function() {
                        c.ContextMenu.beforeContextMenuShow.call(g.contextMenu, {}, g)
                    }});
                this.objectButtons.push(this.menuBtn)
            }
        },initDeleteButton: function() {
            var f = $("delete-btn");
            if (f) {
                APE.EventPublisher.add(f, "onclick", function() {
                    this.onContextDelete()
                }, this);
                this.deleteBtn = new APE.widget.ToggleButton(f);
                this.objectButtons.push(this.deleteBtn)
            }
        },initContextMenu: function() {
            c.ContextMenu.initialize(this)
        },_createSubmitHandlerFor: function(f) {
            var g = this;
            return function() {
                if (this.newImageForm) {
                    this.newImageForm.disable()
                }
                if (this[f + "Field"]) {
                    this[f + "Field"].enable()
                }
                this.populateSurfacesData();
                this.isDesignSaved = true;
                this.saveProjectForm.submit();
                return false
            }
        },initAddToCart: function() {
            this.addToCartBtn = $("add-to-cart-btn");
            if (this.addToCartBtn) {
                APE.EventPublisher.add(this.addToCartBtn, "onclick", this._createSubmitHandlerFor("addToCart"), this)
            }
        },initSellThisItem: function() {
            if (this.sellThisItemBtn) {
                APE.EventPublisher.add(this.sellThisItemBtn, "onclick", this._createSubmitHandlerFor("sellThisItem"), this)
            }
        },initInPlaceEditor: function() {
            this.designTitleField = $("product-attributes-title");
            if (this.designTitleField) {
                var f = this.localizedStrings, g = APE.widget.InPlaceEditor;
                Object.extend(g.config, {replacementTitleText: f.DESIGN_TITLE_ALT_TEXT,cancelText: f.IN_PLACE_EDITOR_CANCEL_TEXT,okText: f.IN_PLACE_EDITOR_OK_TEXT,defaultValue: f.UNNAMED});
                new g(this.designTitleField)
            }
        },positionImageUploadForm: function() {
            if (this.newImageForm) {
                var i = this.newImageForm.previous("h3");
                var h = new Element("div", {className: "new-image-wrapper"});
                h.insert(i);
                h.insert(this.newImageForm);
                var f = this.imagesThumbnailsEl;
                if (f) {
                    var g = f.up(".thumbnails").down("p");
                    if (g) {
                        g.insert({after: h})
                    }
                }
            }
        },initUIControls: function() {
            if (this.controlsWrapper) {
                this.controlsWrapper.show()
            }
        },initDesignsThumbnails: function() {
            var f = this;
            a.observe("click", function(h, g) {
                if ((g = h.findElement("#designs-thumbnails li a"))) {
                    h.stop();
                    f.loadingDesignHref = g.href;
                    f.confirm(f.localizedStrings.LOAD_NEW_DESIGN_CONFIRM_MESSAGE, (function(i) {
                        return function(j) {
                            if (j) {
                                f.loadDesignFromUrl(i)
                            }
                        }
                    })(g.href))
                }
            })
        },loadDesignFromUrl: function(f) {
            f = this.loadingDesignHref;
            this.canvasContainerEl.setOpacity(0);
            this.canvasWrapperEl.setOpacity(0.5);
            this.showLoadingMessage();
            new Ajax.Request(f, {method: "GET",onSuccess: this.onLoadDesignSuccess.bind(this)});
            this.history.clear()
        },onLoadDesignSuccess: function(f) {
            if (f && f.responseJSON) {
                this.loadFromDatalessJSON(f.responseJSON, (function() {
                    this.canvas.renderAll();
                    this.hideLoadingMessage();
                    this.canvas.calcOffset();
                    emile(this.canvasContainerEl, "opacity:1");
                    emile(this.canvasWrapperEl, "opacity:1")
                }).bind(this))
            }
        },initDesignTitleField: function() {
            var g = this;
            if (this.designTitleField) {
                var f = this.designTitleField.value;
                this.designTitleField.observe("keyup", function(h) {
                    if (f !== h.element().value) {
                        g.titleChanged = true;
                        g.renderAppButtons();
                        g.designTitleField.stopObserving()
                    }
                })
            }
        },toDataURL: function(g, h) {
            var f = this.canvas.shouldDrawGuidelines;
            this.canvas.shouldDrawGuidelines = false;
            this.canvas.renderAll();
            this.canvas._toDataURL(g, h);
            this.canvas.shouldDrawGuidelines = f
        },toDataURLWithMultiplier: function(g, i, h) {
            var f = this.canvas.shouldDrawGuidelines;
            this.canvas.shouldDrawGuidelines = false;
            this.canvas.renderAll();
            this.canvas._toDataURLWithMultiplier(g, i, h);
            this.canvas.shouldDrawGuidelines = f
        },initUnselectableElements: function() {
            $$(".unselectable, .btn").each(fabric.util.makeElementUnselectable);
            var f = this;
            this.canvas.observe("mouse:down", function() {
                fabric.util.makeElementUnselectable(a.body);
                f.isDraggingObject = true
            });
            this.canvas.observe("mouse:up", function() {
                fabric.util.makeElementSelectable(a.body);
                setTimeout(function() {
                    f.isDraggingObject = false
                }, 100)
            })
        },getOffsetForColorpicker: function() {
            if (!this.activeColorPickerContainer) {
                return 0
            }
            var f = -this.activeColorPickerContainer.offsetWidth + $("active-color-btn").offsetWidth;
            if (Math.abs(f) > $("active-color-btn").cumulativeOffset().left) {
                f = 0
            }
            return f
        },initActiveColorBtn: function() {
            var f = this;
            if ($("active-color-btn")) {
                this.activeColorBtn = new APE.widget.MenuButton("active-color-btn", "cp1_Container", {labelEl: $$('[for="active-color-btn"]')[0],menuOffsetX: this.getOffsetForColorpicker(),onShow: function() {
                        f.activeColorPicker.reflow()
                    }});
                this.objectButtons.push(this.activeColorBtn)
            }
            this.canvas.color = this.INITIAL_BG_COLOR
        },initActiveOpacityBtn: function() {
            this.opacityBtn = $("active-opacity-btn");
            this.activeOpacityContainer = $("active-opacity-container");
            if (this.opacityBtn) {
                this.activeOpacityBtn = new APE.widget.MenuButton(this.opacityBtn, this.activeOpacityContainer, {labelEl: $$('[for="active-opacity-btn"]')[0]});
                this.activeOpacityBtn.disable();
                this.objectButtons.push(this.activeOpacityBtn)
            }
        },initOpacitySlider: function() {
            if (!$("active-opacity-handle")) {
                return
            }
            this.opacitySliderPreviousValue = 1;
            this.activeOpacitySlider = new Control.Slider("active-opacity-handle", "active-opacity", {onSlide: this.onOpacitySliderSlide.bind(this),onChange: this.onOpacitySliderChange.bind(this),sliderValue: this.opacitySliderPreviousValue,axis: "vertical"})
        },initActiveFontsizeBtn: function() {
            this.fontsizeBtn = $("active-fontsize-btn");
            this.activeFontsizeContainer = $("active-fontsize-container");
            if (this.fontsizeBtn && this.activeFontsizeContainer) {
                this.activeFontsizeBtn = new APE.widget.MenuButton(this.fontsizeBtn, this.activeFontsizeContainer, {labelEl: $$('[for="active-fontsize-btn"]')[0]});
                this.activeFontsizeBtn.disable();
                this.objectButtons.push(this.activeFontsizeBtn)
            }
        },initFontsizeSlider: function() {
            if (!$("active-fontsize-handle")) {
                return
            }
            this.fontsizeSliderPreviousValue = 10;
            this.activeFontsizeSlider = new Control.Slider("active-fontsize-handle", "active-fontsize", {onSlide: this.onFontsizeSliderSlide.bind(this),onChange: this.onFontsizeSliderChange.bind(this),sliderValue: this.fontsizeSliderPreviousValue,range: $R(1, 50)})
        },initFontOptionsMenu: function() {
            var f = this;
            this.fontOptionsMenu = c.FontOptionsMenu.init({onMenuItemChosen: (function(i) {
                    var h = this.getActive();
                    if (h && h.type === "text") {
                        i(h);
                        this.canvas.renderAll();
                        this.fontOptionsBtn.deactivate()
                    }
                }).bind(this)});
            this.fontOptionsBtn = c.FontOptionsBtn.init(this.fontOptionsMenu, function g() {
                return f.getActive()
            });
            this.objectButtons.push(this.fontOptionsBtn)
        },initLineHeightMenu: function() {
            var f = this;
            this.lineHeightMenu = c.LineHeightMenu.init({onMenuItemChosen: (function(i) {
                    var h = this.getActive();
                    if (h && h.type === "text") {
                        i(h);
                        this.canvas.renderAll();
                        this.lineHeightBtn.deactivate()
                    }
                }).bind(this)});
            this.lineHeightBtn = c.LineHeightBtn.init(this.lineHeightMenu, function g() {
                return f.getActive()
            });
            this.objectButtons.push(this.lineHeightBtn)
        },initTextAlignMenu: function() {
            var f = this;
            this.textAlignMenu = c.TextAlignMenu.init({onMenuItemChosen: (function(j, i) {
                    var h = this.getActive();
                    if (h && h.type === "text") {
                        j(h);
                        this.textAlignBtn.buttonEl.down("span").update(i.down(".pseudo-icon").cloneNode(true));
                        this.canvas.renderAll();
                        this.textAlignBtn.deactivate()
                    }
                }).bind(this)});
            this.textAlignBtn = c.TextAlignBtn.init(this.textAlignMenu, function g() {
                return f.getActive()
            });
            this.objectButtons.push(this.textAlignBtn)
        },initTextBgColorBtn: function() {
            var f = this;
            if (!$("text-bg-color-btn") || !$("cp3_Container")) {
                return
            }
            this.textBgColorBtn = new APE.widget.MenuButton("text-bg-color-btn", "cp3_Container", {menuOffsetX: this.getOffsetForColorpicker(),onShow: function() {
                    f.textBgColorPicker.reflow()
                }});
            this.objectButtons.push(this.textBgColorBtn)
        },initMenuHiding: function() {
            var f;
            a.observe("menu:show", function(g) {
                if (f && f !== g.memo.instance) {
                    if (f.deactivate) {
                        f.deactivate()
                    } else {
                        if (f.hide) {
                            f.hide()
                        }
                    }
                }
                f = g.memo.instance
            })
        },onOpacitySliderSlide: function(h) {
            var g = this.getActive(), f = this.activeOpacityLabel.firstChild;
            if (!g) {
                return
            }
            h = 1 * (h.toFixed(2));
            g.set("opacity", h);
            this.canvas.renderAll();
            f.nodeValue = (h * 100) | 0
        },onOpacitySliderChange: function(g) {
            var f = +(g.toFixed(2)), h = this.canvas.getActiveObject();
            if (h && this.opacitySliderPreviousValue !== f) {
                h.set("opacity", f).saveState();
                this.history.add(new fabric.TransformCommand(h));
                this.opacitySliderPreviousValue = f
            }
        },onFontsizeSliderSlide: function(h) {
            var g = this.getActive(), f = this.activeFontsizeLabel.firstChild;
            if (!g) {
                return
            }
            g.scale(h);
            this.canvas.renderAll();
            f.nodeValue = h | 0
        },onFontsizeSliderChange: function(f) {
            var g = this.canvas.getActiveObject();
            if (g && this.fontsizeSliderPreviousValue !== f) {
                g.scale(f).setCoords().saveState();
                this.history.add(new fabric.TransformCommand(g));
                this.fontsizeSliderPreviousValue = f
            }
        },initOnUnloadCleanup: function() {
            var g = this, f = (typeof d.addEventListener == "undefined" && typeof d.attachEvent != "undefined");
            if (!f) {
                return
            }
            Event.observe(d, "unload", function() {
                g.canvas.dispose();
                g.history && g.history.clear();
                for (var k in g.fontDefinitions) {
                    for (var h in g.fontDefinitions[k]) {
                        g.fontDefinitions[k][h] = null
                    }
                    g.fontDefinitions[k] = null
                }
                for (var k in g.shapesCache) {
                    g.shapesCache[k] = null
                }
                g.canvas = g.history = g.shapesCache = g.fontDefinitions = null;
                g = null
            })
        },initOnBeforeUnloadWarning: function() {
            var f = this;
            d.onbeforeunload = function() {
                if (!f.isCanvasEmpty() && !f.isDesignSaved) {
                    return f.localizedStrings.LEAVE_PAGE_WARNING
                }
            }
        },loadFromJSON: function(f) {
            if (f) {
                this.canvas.loadFromJSON(f);
                var g = JSON.parse(f);
                if (g && g.background) {
                    this.setBgColor(g.background)
                }
            }
        },initFocusBlurObserver: function() {
            var h = this;
            function f() {
                h.textFocused = true
            }
            function g() {
                h.textFocused = false
            }
            a.observe("textarea:focused", f).observe("textarea:blurred", g);
            $$('select, input[type="text"], textarea').invoke("observe", "focus", f).invoke("observe", "blur", g)
        },loadSVGFromURL: function(f, g) {
            fabric.loadSVGFromURL(f, g)
        },updateTextPlaceholder: function(g, f) {
            if (this.textPlaceholder) {
                this.textPlaceholder.activate();
                this.textPlaceholder.setValue(g);
                this.textPlaceholder.lastValue = g;
                this.updateChooseFontBtn(f)
            }
        },updateChooseFontBtn: function(f) {
            $("choose-font-btn").down("span").innerHTML = f.truncate(20)
        },onObjectSelected: function(h) {
            var g = h.memo.target;
            this.enableObjectButtons();
            if (g.isType("text")) {
                this.onTextObjectSelected(g)
            } else {
                this.activeFontsizeBtn && this.activeFontsizeBtn.disable();
                this.textPopup.hide()
            }
            if (this.shouldDisableColorSelection(g)) {
                this.activeColorBtn && this.activeColorBtn.disable()
            } else {
                this.activeColorPicker && this.activeColorPicker.setValueFromAny(g.get(g.freeDrawn ? "stroke" : "fill"))
            }
            var f = g.get("opacity");
            this.activeOpacitySlider && this.activeOpacitySlider.setValue(f, 0, b);
            this.activeOpacityLabel && (this.activeOpacityLabel.firstChild.nodeValue = fabric.util.toFixed(f * 100, 2));
            this.activeObjectProps.update()
        },shouldDisableColorSelection: function(f) {
            return (f.isType("image") || (f.isType("path-group") && !f.isSameColor()) || (f.isType("group") && f.hasFreeDrawn && f.hasFreeDrawn()))
        },onTextObjectSelected: function(h) {
            var f = this.textPopup.element.visible();
            this.textPopup.show();
            this.updateTextPlaceholder(h.get("text"), h.get("fontFamily"));
            var j = h.get("scaleX");
            if (this.activeFontsizeSlider) {
                this.activeFontsizeSlider.setValue(j, 0, b);
                this.activeFontsizeLabel.firstChild.nodeValue = j | 0;
                this.activeFontsizeBtn.enable()
            }
            var i = this.textAlignMenu.container.down(".pseudo-icon." + h.textAlign);
            this.textAlignBtn.buttonEl.down("span").update(i.cloneNode(true));
            var g = h.get("backgroundColor") || "transparent";
            if (g !== "transparent") {
                g = new fabric.Color(h.get("backgroundColor")).toHex()
            }
            this.textBgColorPicker.setValueFromHex(g, b);
            this.removeTextBgColorEl.checked = false;
            if (!f) {
                setTimeout(this.textPopup.selectText.bind(this.textPopup), 100)
            }
        },initActiveControlObserver: function() {
            var f = this;
            this.canvas.observe("object:selected", this.onObjectSelected.bind(this));
            this.canvas.observe("group:selected", function(h) {
                f.activeColorBtn && f.activeColorBtn.disable();
                f.onObjectSelected(h);
                var g = h.memo.target;
                f.history.add(new fabric.GroupAddCommand(g, f))
            });
            this.canvas.observe("before:group:destroyed", function(h) {
                var g = h.memo.target;
                f.history.add(new fabric.GroupRemoveCommand(g, f))
            });
            this.canvas.observe("selection:cleared", this.onSelectionCleared.bind(this));
            this.disableObjectButtons()
        },onSelectionCleared: function(f) {
            this.disableObjectButtons();
            if (this.textPlaceholder) {
                this.textPlaceholder.deactivate()
            }
            this.activeObjectProps.set();
            this.textPopup.hide();
            this.canvas.getObjects().each(function(g) {
                if (g.type === "text" && g.text === "") {
                    this.removeObject(g, b)
                }
            }, this)
        },initHistory: function() {
            var f = this;
            if (!f.undoBtn || !f.redoBtn) {
                return
            }
            this.history = new fabric.CommandHistory({backTrigger: f.undoBtn,forwardTrigger: f.redoBtn});
            this.canvas.observe("object:modified", function(g) {
                f.history.add(new fabric.TransformCommand(g.memo.target))
            });
            this.undoBtn.observe("click", function(g) {
                g.stop();
                f.history.back();
                f.canvas.renderAll()
            });
            this.redoBtn.observe("click", function(g) {
                g.stop();
                f.history.forward();
                f.canvas.renderAll()
            });
            a.observe(fabric.CommandHistory.CHANGE_EVENT, this.onHistoryChange.bind(this));
            this.renderAppButtons()
        },onHistoryChange: function() {
            a.fire("content:changed");
            this.renderAppButtons();
            var g = this.canvas.getActiveObject(), f = true;
            if (g) {
                if (this.activeColorPicker) {
                    this.activeColorPicker.setValueFromAny(g.get(g.freeDrawn ? "stroke" : "fill"))
                }
                if (this.activeOpacitySlider) {
                    this.activeOpacitySlider.setValue(g.get("opacity"), 0, f)
                }
                if (g.isType("text")) {
                    this.textPlaceholder.setValue(g.getText())
                }
            }
        },disableAppButtons: function() {
            this.appButtons.each(function(f) {
                f && f.disable().addClassName("disabled")
            })
        },enableAppButtons: function() {
            this.appButtons.each(function(f) {
                f && f.enable().removeClassName("disabled")
            })
        },renderAppButtons: function() {
            if (this.isCanvasEmpty()) {
                if (this.titleChanged) {
                    this.enableAppButtons()
                } else {
                    this.disableAppButtons()
                }
            } else {
                this.enableAppButtons()
            }
        },initSaveDesign: function() {
            var f = this;
            this.shouldSubmitForm = !!$("should-submit-form").value;
            this.isDesignSaved = true;
            this.addToCartField && this.addToCartField.disable();
            this.sellThisItemField && this.sellThisItemField.disable();
            if (!this.saveProjectForm) {
                return
            }
            this.saveProjectForm.observe("submit", function(g) {
                g.stop();
                if (f.shouldSubmitForm) {
                    f.saveDesignViaForm()
                } else {
                    f.saveDesignViaXhr()
                }
            })
        },isCanvasEmpty: function() {
            return !c.Surfaces.hasData()
        },populateSurfacesData: function() {
            if (c.Surfaces.dataPlaceholders) {
                c.Surfaces.populate.call(this)
            }
        },saveDesignViaForm: function() {
            this.populateSurfacesData();
            this.isDesignSaved = true;
            this.saveProjectForm.submit()
        },saveDesignViaXhr: function(g) {
            this.populateSurfacesData();
            if (this.savingDesign) {
                return
            }
            this.savingDesign = true;
            if (this.newImageForm) {
                this.newImageForm.disable()
            }
            this.statusMessage.setStyle(this.STATUS_MESSAGE_STYLES);
            this.statusMessage.innerHTML = this.localizedStrings.SAVING;
            this.statusMessage.show();
            var f = this.saveProjectForm.action;
            this.prepareSaveFormUrl();
            this.sendSaveDesignXHR(g);
            this.saveProjectForm.action = f;
            c.Surfaces.populateWithOriginalData();
            if (this.newImageForm) {
                this.newImageForm.enable()
            }
            if (this.addToCartField) {
                this.addToCartField.disable()
            }
        },prepareSaveFormUrl: function() {
            if (typeof this.DESIGN_ID == "number") {
                options.method = "PUT";
                this.saveProjectForm.action += ("/" + this.DESIGN_ID)
            }
            this.saveProjectForm.action += ".json"
        },sendSaveDesignXHR: function(h) {
            this.saveProjectBtn = $("save-design");
            var g = this.saveProjectBtn.innerHTML;
            var f = {onSuccess: this.onSuccessSaveDesign.bind(this),onFailure: this.onFailureSaveDesign.bind(this),onCreate: (function() {
                    this.saveProjectBtn.update(this.localizedStrings.SAVING).disable()
                }).bind(this),onComplete: (function() {
                    this.saveProjectBtn.update(g).enable();
                    this.savingDesign = false;
                    h && h()
                }).bind(this)};
            this.saveProjectForm.request(f)
        },onSaveDesign: function(h, g, f) {
            var i = this;
            this.statusMessage.style.backgroundPosition = "-9999px 50%";
            emile(this.statusMessage, "width:200px;background-color:" + g, {duration: 500,after: function() {
                    i.statusMessage.innerHTML = h;
                    d.setTimeout(function() {
                        Element.fade(i.statusMessage, {duration: 500})
                    }, f)
                }})
        },onSuccessSaveDesign: function(g) {
            var f = g.responseJSON;
            if (f) {
                if (f.design && f.design.id) {
                    this.DESIGN_ID = f.design.id
                } else {
                    if (f.item && f.item.id) {
                        this.DESIGN_ID = f.item.id;
                        this.initializeSurfaceData(f.item)
                    }
                }
            }
            this.isDesignSaved = true;
            this.onSaveDesign(this.localizedStrings.SUCCESS_SAVED_TEXT, this.SUCCESS_COLOR, 1000)
        },onFailureSaveDesign: function(h) {
            var f = "";
            if (h.responseText) {
                var g = JSON.parse(h.responseText);
                if (g && g[0]) {
                    f = g[0]
                }
            }
            this.onSaveDesign(this.localizedStrings.SOMETHING_WRONG + "<br>" + f, this.ERROR_COLOR, 3000)
        },initializeSurfaceData: function(h) {
            if (!this.SURFACE_DATA_INITIALIZED && Object.isArray(h.surfaces)) {
                var f = jsonSurfaceDataToFormFields(h), g = this.saveProjectForm;
                f.each(function(i) {
                    g.appendChild(i)
                });
                this.SURFACE_DATA_INITIALIZED = true
            }
        },jsonSurfaceDataToFormFields: function(g) {
            var f = [], h;
            g.surfaces.each(function(j, k) {
                if ("id" in j) {
                    h = new Element("input", {type: "hidden",name: "item[surfaces_attributes][" + k + "][id]",value: j.id})
                }
                f.push(h);
                if (j.design && "id" in j.design) {
                    h = new Element("input", {type: "hidden",name: "item[surfaces_attributes][" + k + "][design_attributes][id]",value: j.design.id})
                }
                f.push(h)
            });
            return f
        },initClearCanvas: function() {
            this.clearCanvasBtn = $("clear-canvas");
            if (!this.clearCanvasBtn) {
                return
            }
            var f = this;
            this.clearCanvasBtn.observe("click", function(g) {
                g.stop();
                f.confirm(f.localizedStrings.CLEAR_CANVAS_CONFIRM_MESSAGE, function(h) {
                    if (h) {
                        f.canvas.clear();
                        f.history.clear();
                        c.Surfaces.save();
                        a.fire("content:changed")
                    }
                })
            })
        },getActive: function() {
            return this.canvas.getActiveGroup() || this.canvas.getActiveObject()
        },removeActive: function() {
            var f = this.getActive();
            if (f) {
                this[f.isType("group") ? "removeGroup" : "removeObject"](f);
                this.canvas.renderAll();
                this.activeObjectProps.set()
            }
        },disableObjectButtons: function() {
            this.objectButtons.invoke("disable")
        },enableObjectButtons: function() {
            this.objectButtons.invoke("enable")
        },initCanvasBgColorPicker: function() {
            if (!$("bg-color-btn")) {
                return
            }
            var h = this;
            APE.widget.PalettePicker.config.cellTitle = this.localizedStrings.PALETTE_PICKER_CELL_TITLE;
            this.canvasBgColorPicker = new APE.widget.PalettePicker();
            this.canvasBgColorPicker.appendTo(a.body);
            this.canvasBgColorPicker.onSelect = function(j) {
                var i = j.getStyle("background-color");
                a.fire("color:changed", {color: i});
                h.setBgColor(i)
            };
            var g = this.canvasBgColorPicker.getContainerEl();
            g.id = "bg-colorpicker";
            g.style.cssText = "visibility:hidden;top:0;left:-9999px";
            var f = this.canvasBgColorBtn = new APE.widget.MenuButton("canvas-color-btn", g.id, {labelEl: $$('label[for="canvas-color-btn"]')[0]});
            f.enable();
            f.labelEl.up().show();
            this.loadBgColorPickerPalette()
        },loadBgColorPickerPalette: function() {
            var f = JSON.parse($("product-meta").value).colors.map((function(g) {
                return this.colorMap[g]
            }).bind(this));
            if (Object.isArray(f)) {
                if (f.length > 1) {
                    this.canvasBgColorPicker.loadPalette(f)
                } else {
                    this.canvasBgColorBtn.hide()
                }
            }
        },showLoadingMessage: function() {
            this.statusMessage.setStyle(this.STATUS_MESSAGE_STYLES);
            this.statusMessage.innerHTML = this.localizedStrings.LOADING;
            this.statusMessage.show()
        },hideLoadingMessage: function() {
            this.statusMessage.hide()
        },normalizeBgColor: function(f) {
            var i = this, g = new fabric.Color(f).toHex(), h = typeof g == "undefined";
            g = "#" + g.toLowerCase();
            if (!h) {
                return (function() {
                    for (var j in i.colorMap) {
                        if (i.colorMap[j].toLowerCase() === g) {
                            return j
                        }
                    }
                    return ""
                })()
            }
            return f
        },setWrappersColor: function(f) {
            f = this.normalizeBgColor(f);
            this.showLoadingMessage();
            this.loadBgImage(f);
            this.productMeta.designs.each(function(i) {
                var j = $("preview-" + i.side);
                if (j) {
                    var h = i.layers.background.interpolate({style: "small",color: f});
                    j.down("img").src = h
                }
            });
            if (this.colorHiddenFieldEl) {
                var g = this.colorHiddenFieldEl.value;
                this.colorHiddenFieldEl.value = f
            }
            a.fire("product:color:changed", {curValue: f,prevValue: g})
        },loadBgImage: function(h) {
            var j = new Image(), g = $("canvas-bg"), i = this.getCurrentSideMetadata(), k = this;
            var f = i.layers.background.interpolate({style: "large",color: h});
            g.ondragstart = function() {
                return false
            };
            j.style.display = "none";
            j.onload = function() {
                g.src = f;
                k.hideLoadingMessage();
                a.body.removeChild(j);
                j = j.onload = null;
                setTimeout(function() {
                    k.canvas.calcOffset()
                }, 100)
            };
            a.body.appendChild(j);
            j.src = f
        },setDefaultSide: function() {
            this.currentSide = this.productMeta.default_side
        },setCurrentSide: function(f) {
            this.currentSide = f
        },getMetadataForSide: function(f) {
            return this.productMeta.designs.find(function(g) {
                return g.side === f
            })
        },getCurrentSideMetadata: function() {
            return this.getMetadataForSide(this.currentSide)
        },setBgColor: function(g, f) {
            if (!this.CAN_SET_BG_COLOR) {
                return
            }
            if (this.colorMap[g]) {
                g = this.colorMap[g]
            }
            var h = this.getCommandForSetBgColor(g);
            h.execute();
            if (this.history && !f) {
                this.history.add(h)
            }
            this.currentBgColor = g
        },getCommandForSetBgColor: function(f) {
            var h = this, g = this.canvasWrapperEl.style.backgroundColor;
            return ({execute: function() {
                    h.canvasBgPreviewEl && (h.canvasBgPreviewEl.style.backgroundColor = f);
                    h.setWrappersColor(f);
                    h.canvas.renderAll()
                },undo: function() {
                    h.canvasBgPreviewEl && (h.canvasBgPreviewEl.style.backgroundColor = g);
                    h.setWrappersColor(g);
                    h.canvas.renderAll()
                }})
        },initActiveColorPicker: function() {
            this.activeColorPreviewEl = $("cp1_Preview");
            if (!this.activeColorPreviewEl) {
                return
            }
            this.activeColorPicker = new Refresh.Web.ColorPicker("cp1", {startMode: "h",clientFilesPath: this.COLORPICKER_IMAGES_PATH,controlsOn: false});
            this.activeColorPicker.hide();
            this.activeColorPicker.updateVisuals();
            this.canvas.calcOffset()
        },initTextBgColorPicker: function() {
            this.textBgColorPreviewEl = $("cp3_Preview");
            if (!this.textBgColorPreviewEl || !$("cp3_Container")) {
                return
            }
            this.textBgColorPicker = new Refresh.Web.ColorPicker("cp3", {startMode: "h",clientFilesPath: this.COLORPICKER_IMAGES_PATH,controlsOn: false});
            this.textBgColorPicker.hide();
            this.textBgColorPicker.updateVisuals();
            this.canvas.calcOffset()
        },initRemoveBg: function() {
            function f(h) {
                return function() {
                    if (this.checked) {
                        g[h].setTransparent();
                        a.fire("color:did:change", {color: {hex: "transparent"},instance: g[h]})
                    } else {
                        g[h].unsetTransparent();
                        a.fire("color:did:change", {color: g[h]._cvp.color,instance: g[h]})
                    }
                }
            }
            var g = this;
            this.removeTextBgColorEl = $("remove-text-bg-color");
            this.removeTextBgColorEl && this.removeTextBgColorEl.observe("click", f("textBgColorPicker"));
            this.removeItemBgColorEl = $("remove-item-bg-color");
            this.removeItemBgColorEl && this.removeItemBgColorEl.observe("click", f("bgColorPicker"))
        },loadImageFromURL: function(f, h) {
            var g = this;
            this.showLoadingMessage();
            fabric.util.loadImage(f, function(i) {
                g.hideLoadingMessage();
                h(new fabric.Image(i))
            })
        },initColorSelection: function() {
            a.observe("color:will:change", this.onColorWillChange.bind(this));
            a.observe("color:did:change", this.onColorDidChange.bind(this));
            a.observe("color:selected", this.onColorSelected.bind(this))
        },onColorWillChange: function() {
            var f = this.getActive();
            if (f) {
                f.saveState()
            }
        },onColorDidChange: function(l) {
            var g = l.memo.color, f = l.memo.instance, i = this.getActive(), h = f.id === "cp2", j = f.id === "cp3";
            function k(m) {
                if (m === "transparent" || m === "") {
                    return "transparent"
                }
                return "#" + m
            }
            if (h) {
                this.canvas.backgroundColor = k(g.hex);
                this.canvas.renderAll();
                return
            }
            if (j) {
                if (i && i.type === "text") {
                    i.backgroundColor = k(g.hex);
                    this.canvas.renderAll()
                }
                return
            }
            if (i) {
                i.set(i.freeDrawn ? "stroke" : "fill", "#" + (g.hex || ""));
                this.canvas.renderAll()
            }
            if (this.canvas.isDrawingMode) {
                this.canvas.freeDrawingColor = "#" + g.hex
            }
        },onColorSelected: function(f) {
            if (f.target.up().id === "cp3_ColorMap") {
                return
            }
            var g = this;
            setTimeout(function() {
                var h = g.getActive();
                if (h && (h.get("fill") !== "rgb(0,0,0)")) {
                    g.canvas.fire("object:modified", {target: g.getActive()})
                }
            }, 10)
        },addToGroup: function(f) {
            var g = this.canvas.getActiveGroup();
            if (g) {
                g.add(f);
                f.setActive(true);
                this.canvas.renderAll()
            }
        },addObject: function(i, f, h) {
            var g = arguments.length === 3;
            if (g) {
                this.canvas.insertAt(i, h)
            } else {
                this.canvas.add(i)
            }
            if (!f && this.history) {
                a.fire("content:changed");
                this.history.add(new fabric.AddCommand(i, this))
            }
            i.setCoords();
            this.adjustPerformance();
            this.renderAppButtons()
        },addObjectRandomly: function(g) {
            var h = 50, f = this.canvas.getCenter();
            g.set("left", f.left + fabric.util.getRandomInt(-h, h)).set("top", f.top + fabric.util.getRandomInt(-h, h));
            this.addObject(g)
        },resizeObjectToFit: function(f, h, g) {
            this.canvas.centerObjectH(f).centerObjectV(f);
            if (f.isType("image")) {
                this.resizeObjectPerDPI(f, h, g)
            } else {
                f.scaleToWidth(this.canvas.getWidth() - this.SCALE_MARGIN);
                if (f.getHeight() > this.canvas.getHeight()) {
                    f.scaleToHeight(this.canvas.getHeight() - this.SCALE_MARGIN)
                }
                this.canvas.setActiveObject(f.setCoords())
            }
            return this
        },getHeightForCurrentSideDPI: function(h, f, g) {
            return h.originalHeight / f * g
        },getWidthForCurrentSideDPI: function(h, f, g) {
            return h.originalWidth / f * g
        },findBestUsableHeight: function(f) {
            return this._findBestUsable("Height", f)
        },findBestUsableWidth: function(f) {
            return this._findBestUsable("Width", f)
        },_findBestUsable: function(n, g) {
            var k = 200, h = 100, l = 50, q = 20, m = this.getCurrentSideMetadata().dpi, p = "get" + n + "ForCurrentSideDPI";
            for (var j = k; j >= h; j -= l) {
                var o = this[p](g, j, m);
                if (o > q) {
                    return o
                }
            }
            var f = this[p](g, h, m);
            return f
        },findSideToScaleBy: function(f) {
            return this.canvas.getWidth() > this.canvas.getHeight() ? "Height" : "Width"
        },resizeObjectPerDPI: function(g, k, i) {
            g.set("originalWidth", parseInt(k)).set("originalHeight", parseInt(i));
            var h = this.findSideToScaleBy(g), f = this["findBestUsable" + h](g), j = this.canvas["get" + h]();
            g["scaleTo" + h](Math.min(f, j - 20));
            this.canvas.renderAll()
        },removeObject: function(g, f, i) {
            var h = g.get("opacity");
            if (this.ANIMATION_ENABLED) {
                this.canvas.fxRemove(g, (function() {
                    if (i) {
                        i.apply(null, arguments)
                    }
                    if (!this.canvas.getActiveGroup() || !this.canvas.getActiveGroup().contains(g)) {
                        g.setOpacity(h)
                    }
                    this.afterObjectRemoved(g, f)
                }).bind(this))
            } else {
                this.canvas.remove(g);
                this.afterObjectRemoved(g, f)
            }
            this.disableObjectButtons()
        },afterObjectRemoved: function(g, f) {
            if (!f) {
                a.fire("content:changed");
                this.history.add(new fabric.RemoveCommand(g, this))
            }
            if (g.type === "text") {
                this.textPlaceholder.setValue("");
                this.textPopup.hide()
            }
            this.adjustPerformance();
            this.renderAppButtons()
        },straightenObject: function(g, f) {
            if (!g) {
                return
            }
            this.canvas[this.ANIMATION_ENABLED ? "fxStraightenObject" : "straightenObject"](g);
            if (!f) {
                this.history.add(new fabric.TransformCommand(g))
            }
        },sendObjectToBack: function(g, f) {
            if (!g) {
                return
            }
            if (g.isType("group")) {
            } else {
                this.canvas.sendToBack(g);
                if (!f) {
                    this.history.add(new fabric.TransformCommand(g))
                }
            }
        },bringObjectToFront: function(g, f) {
            if (!g) {
                return
            }
            if (g.isType("group")) {
            } else {
                this.canvas.bringToFront(g);
                if (!f) {
                    this.history.add(new fabric.TransformCommand(g))
                }
            }
        },sendObjectBackwards: function(g, f) {
            if (!g) {
                return
            }
            if (g.isType("group")) {
            } else {
                this.canvas.sendBackwards(g);
                if (!f) {
                    this.history.add(new fabric.TransformCommand(g))
                }
            }
        },bringObjectForward: function(g, f) {
            if (!g) {
                return
            }
            if (g.isType("group")) {
            } else {
                this.canvas.bringForward(g);
                if (!f) {
                    this.history.add(new fabric.TransformCommand(g))
                }
            }
        },straightenActiveObject: function() {
            this.straightenObject(this.getActive())
        },sendActiveObjectToBack: function() {
            this.sendObjectToBack(this.getActive())
        },bringActiveObjectToFront: function() {
            this.bringObjectToFront(this.getActive())
        },sendActiveObjectBackwards: function() {
            this.sendObjectBackwards(this.getActive())
        },bringActiveObjectForward: function() {
            this.bringObjectForward(this.getActive())
        },resizeActiveObjectToFit: function() {
            var f = this.getActive();
            if (f) {
                f.saveState().scaleToWidth(this.canvas.CANVAS_WIDTH - this.SCALE_MARGIN);
                this.history.add(new fabric.TransformCommand(f));
                this.canvas.renderAll()
            }
        },flipActiveObjectVertically: function() {
            var f = this.getActive();
            if (f) {
                f.saveState().toggle("flipY");
                this.history.add(new fabric.TransformCommand(f));
                this.canvas.renderAll()
            }
        },flipActiveObjectHorizontally: function() {
            var f = this.getActive();
            if (f) {
                f.saveState().toggle("flipX");
                this.history.add(new fabric.TransformCommand(f));
                this.renderAll()
            }
        },cloneActiveObject: function() {
            var i = this, f = this.getActive(), h;
            if (f) {
                var g = this.canvas.getActiveGroup();
                if (g) {
                    this.cloneActiveGroup(g)
                } else {
                    if (f.isType("image")) {
                        f.clone(function(j) {
                            if (!j) {
                                return
                            }
                            i.initClonedObject(j)
                        })
                    } else {
                        h = f.clone();
                        if (!h) {
                            return
                        }
                        i.initClonedObject(h)
                    }
                }
            }
        },initClonedObject: function(f) {
            f.top += this.CLONE_OFFSET;
            f.left += this.CLONE_OFFSET;
            this.addObject(f);
            this.canvas.setActiveObject(f)
        },cloneActiveGroup: function(f) {
            var g = f.clone();
            if (!g) {
                return
            }
            g.set("left", g.get("left") + this.GROUP_CLONE_OFFSET);
            g.set("top", g.get("top") + this.GROUP_CLONE_OFFSET);
            g.getObjects().each(function(h) {
                this.addObject(h, b)
            }, this);
            this.canvas.discardActiveGroup();
            this.deactivateAll();
            this.canvas.setActiveGroup(g);
            g.activateAllObjects();
            this.canvas.renderAll()
        },deleteAllExceptActive: function() {
            var f = this.getActive();
            if (f) {
                var g = this.canvas.getActiveGroup();
                if (g) {
                    this.canvas.getObjects().reject(function(h) {
                        return g.contains(h)
                    }, this).each(function(h) {
                        this.removeObject(h)
                    }, this)
                } else {
                    this.canvas.getObjects().without(f).each(function(h) {
                        this.removeObject(h)
                    }, this)
                }
            }
        },addGroup: function(h, f) {
            function g() {
                var k = [];
                for (var n = h.getObjects(), m = n.length; m--; ) {
                    k[m] = n[m]
                }
                h.getObjects().clear();
                for (var l = k.length; l--; ) {
                    h.add(k[l])
                }
            }
            if (!f) {
                this.history.add(new fabric.GroupAddCommand(h, this))
            }
            g();
            this.canvas.setActiveGroup(h)
        },removeGroup: function(h, g) {
            var f = h.get("opacity");
            h.hideCorners = h.hideBorders = true;
            this.removeGroupObjects(h);
            if (this.ANIMATION_ENABLED) {
                a.observe("group:objects:removed", this.afterGroupRemoved.bind(this, h, f, g))
            } else {
                this.afterGroupRemoved(h, f, g)
            }
            this.disableObjectButtons()
        },removeGroupObjects: function(g) {
            var f = g.size() - 1;
            g.getObjects().each(function(j, h) {
                this.removeObject(j, false, function() {
                    if (h === f) {
                        isRemovalFinished = true;
                        a.fire("group:objects:removed")
                    }
                })
            }, this)
        },afterGroupRemoved: function(h, f, g) {
            a.fire("content:changed");
            this.history.add(new fabric.GroupRemoveCommand(h.setOpacity(f), this));
            if (!g) {
                this.canvas.setActiveGroup(null)
            }
        },initKeyObserver: function() {
            c.KeyObserver.initialize(this)
        },loadFromDatalessJSON: function(f, h) {
            var g = this;
            return this.canvas.loadFromDatalessJSON(f, function() {
                g.applyImageFilters();
                h && h()
            })
        },applyImageFilters: function() {
            this.canvas.forEachObject(function(f) {
                if (f.type === "image" && f.filters.length) {
                    f.applyFilters(this.canvas.renderAll.bind(this.canvas))
                }
            }, this)
        },toDatalessJSON: function() {
            return JSON.stringify(this.canvas.toDatalessJSON())
        },initSurfaces: function() {
            c.Surfaces.create(this).init();
            var f = this.canvas.calcOffset.bind(this.canvas);
            a.observe("surface:changed", (function() {
                this.canvas.discardActiveGroup();
                this.activeObjectProps.set();
                this.textPlaceholder && this.textPlaceholder.deactivate();
                setTimeout(f, 100)
            }).bind(this));
            a.observe("design:loaded", (function() {
                this.updateSavedSurfacesData()
            }).bind(this))
        },adjustPerformance: function() {
            this.ANIMATION_ENABLED = false
        },confirm: function(f, g) {
            c.ConfirmDialog.prompt(f, g)
        },initUploader: function() {
            c.Uploader.initialize();
            c.DragDropUploader.initialize()
        },initFreeDrawing: function() {
            this.freeDrawingBtn = $("free-drawing-btn");
            this.activeColorLabel = $$('[for="active-color-btn"]')[0];
            if (!this.freeDrawingBtn) {
                return
            }
            this.freeDrawingBtn.observe("click", function(f) {
                this[this.canvas.isDrawingMode ? "finishFreeDrawingMode" : "enterFreeDrawingMode"]()
            }.bind(this));
            this.canvas.observe("path:created", function(f) {
                f.memo.path.freeDrawn = true
            });
            a.observe("thumb:drag:started", function() {
                this.finishFreeDrawingMode()
            }.bind(this));
            this.extendForFreeDrawing()
        },extendForFreeDrawing: function() {
            fabric.Path.prototype.toObject = (function(f) {
                return function() {
                    if (this.freeDrawn) {
                        return fabric.util.object.extend(f.call(this), {freeDrawn: this.freeDrawn,fill: null})
                    }
                    return f.call(this)
                }
            })(fabric.Path.prototype.toObject);
            fabric.Path.prototype.stateProperties = fabric.Object.prototype.stateProperties.concat();
            fabric.Path.prototype.stateProperties.push("freeDrawn");
            fabric.Group.prototype.hasFreeDrawn = function() {
                for (var f = this.objects.length; f--; ) {
                    if (this.objects[f].freeDrawn) {
                        return true
                    }
                }
                return false
            }
        },toggleColorObjectLineLabel: function() {
            var f = this.freeDrawingBtn.getAttribute("data-alt-value");
            this.freeDrawingBtn.setAttribute("data-alt-value", this.freeDrawingBtn.innerHTML);
            this.freeDrawingBtn.innerHTML = f
        },toggleFreeDrawingLabel: function() {
            var f = this.activeColorLabel.getAttribute("data-alt-value");
            this.activeColorLabel.setAttribute("data-alt-value", this.activeColorLabel.innerHTML);
            this.activeColorLabel.innerHTML = f
        },enterFreeDrawingMode: function() {
            if (!this.canvas.isDrawingMode) {
                this.canvas.isDrawingMode = true;
                this.freeDrawingBtn.addClassName("selected");
                this.deactivateAll();
                this.toggleColorObjectLineLabel();
                this.toggleFreeDrawingLabel();
                this.canvas.freeDrawingColor = "#" + (this.activeColorPicker.getCurrentColor() || "000000");
                this.canvas.freeDrawingLineWidth = 3;
                this.activeColorBtn.enable();
                this.textPlaceholder.disable();
                this.chooseFontBtn.disable();
                this.canvas.calcOffset();
                this.canvas.renderAll()
            }
        },finishFreeDrawingMode: function() {
            if (this.canvas.isDrawingMode) {
                this.canvas.isDrawingMode = false;
                this.freeDrawingBtn.removeClassName("selected");
                this.toggleColorObjectLineLabel();
                this.toggleFreeDrawingLabel();
                this.activeColorBtn.disable();
                this.textPlaceholder.enable();
                this.chooseFontBtn.enable()
            }
        },initAligningCenteringGuidelines: function() {
            var g = $("toggle-aligning-guidelines"), f = createAligningGuidelines(this.canvas), h = createCenteringGuidelines(this.canvas);
            if (!g) {
                return
            }
            g.observe("click", function() {
                f[this.checked ? "enable" : "disable"]();
                h[this.checked ? "enable" : "disable"]()
            });
            if (g.checked) {
                f.enable();
                h.enable()
            }
        },getCurrentSurfacesData: function() {
            this.populateSurfacesData();
            return c.Surfaces.getAllData()
        },updateSavedSurfacesData: function() {
            this.savedSurfacesData = this.getCurrentSurfacesData()
        },hasSurfacesDataChanged: function() {
            return this.savedSurfacesData !== this.getCurrentSurfacesData()
        }});
    e.Dashboard = c
})();
Dashboard.ScrollingImageLoader = {init: function(a) {
        if (!a) {
            return
        }
        this.containerEl = a;
        this.totalImagesLoaded = 0;
        this.imgEls = a.select(".img");
        a.onscroll = this.onscroll.bind(this);
        this.onscroll()
    },onscroll: function() {
        if (!this.startTime) {
            this.startTime = new Date()
        } else {
            this.timeFromPreviousInvocation = new Date() - this.startTime;
            if (this.timeFromPreviousInvocation < 50) {
                return
            }
            this.startTime = new Date()
        }
        this.checkElementsInView();
        if (this.totalImagesLoaded === this.imgEls.length) {
            this.containerEl.onscroll = null
        }
    },checkElementsInView: function() {
        for (var b = 0, a = this.imgEls.length; b < a; b++) {
            var c = this.imgEls[b];
            if (!c.__loaded && this.isScrolledElementInView(c)) {
                c.src = c.getAttribute("data-src");
                c.__loaded = true;
                this.totalImagesLoaded++
            }
        }
    },isScrolledElementInView: function(b) {
        var c = b.parentNode.parentNode, a = this.containerEl.scrollTop, e = c.offsetTop, d = this.containerEl ? this.containerEl.offsetHeight : 0;
        return (e + c.offsetHeight) > a && e < (a + d)
    }};
Dashboard.RealisticPoller = {POLL_URL: "/item_views.json?ids=#{id}&style=#{style}",POLL_INTERVAL: 2000,init: function(a, b) {
        if (this.isInProgress) {
            return
        }
        if (!a.hasSurfacesDataChanged()) {
            return
        }
        this.isInProgress = true;
        this.dashboard = a;
        this.productId = $("product-id").value;
        this.realisticPreviewWrapper = $("realistic-preview-wrapper");
        this.progressEl = this.realisticPreviewWrapper.down("progress");
        this.appendRasterizingMessages();
        this.updateProgressEl(0);
        this.progressEl.style.visibility = "visible";
        this.progressifier = new Progressify();
        this.pollingDuration = 0;
        this.pollingStartTime = new Date();
        var d = 100;
        var c = setInterval((function() {
            var e = this.progressifier.progressAt((new Date() - this.pollingStartTime) / 1000);
            if (e === 100) {
                clearInterval(c)
            }
            this.updateProgressEl(e)
        }).bind(this), d);
        if (b) {
            this.onComplete()
        } else {
            a.saveDesignViaXhr(this.onComplete.bind(this))
        }
    },onComplete: function onComplete() {
        this.pollTimer = setInterval(this.poll.bind(this), this.POLL_INTERVAL);
        this.poll()
    },poll: function() {
        var b = this.realisticPreviewWrapper.select(".rasterizing-message");
        var a = this.realisticPreviewWrapper.select(".rasterizing-message-small");
        if (b.length > 0) {
            this.pollForLargeUrls()
        }
        if (a.length > 0) {
            this.pollForSmallUrls()
        }
        if (b.length == 0 && a.length == 0) {
            this.finish()
        }
    },pollForLargeUrls: function() {
        var a = this.POLL_URL.interpolate({id: this.productId,style: "closeup"});
        new Ajax.Request(a, {method: "GET",onComplete: (function(b) {
                var e = b.responseJSON;
                var h = e[Object.keys(e)[0]];
                for (var c in h) {
                    if (c === "progress") {
                        this.pollingDuration = new Date() - this.pollingStartTime;
                        this.progressifier.addPoint(this.pollingDuration / 1000, h[c]);
                        continue
                    }
                    if (!h[c]) {
                        continue
                    }
                    var d = $("view-" + c);
                    var g = d.down(".rasterizing-message");
                    var f = d.down("img");
                    if (g) {
                        f.src = h[c];
                        emile(f, "opacity:1", {duration: 300});
                        g.remove()
                    }
                }
            }).bind(this)})
    },pollForSmallUrls: function() {
        var a = this.POLL_URL.interpolate({id: this.productId,style: "small"});
        new Ajax.Request(a, {method: "get",onComplete: (function(b) {
                var e = b.responseJSON;
                var h = e[Object.keys(e)[0]];
                for (var c in h) {
                    if (!h[c]) {
                        continue
                    }
                    if (c === "progress") {
                        continue
                    }
                    var d = $("thumb-" + c);
                    var g = d.down(".rasterizing-message-small");
                    var f = d.down("img");
                    if (g) {
                        f.src = h[c];
                        emile(f, "opacity:1", {duration: 300});
                        g.remove()
                    }
                }
            }).bind(this)})
    },appendRasterizingMessages: function() {
        var b = $$(".rasterizing-message")[0];
        var a = $$(".rasterizing-message-small")[0];
        this.realisticPreviewWrapper.select(".view").each(function(c) {
            if (!c.down(".rasterizing-message")) {
                c.appendChild(b.cloneNode(true))
            }
            c.down("img").setOpacity(0.3)
        });
        this.realisticPreviewWrapper.select(".thumb .wrapper").each(function(c) {
            if (!c.down(".rasterizing-message-small")) {
                c.appendChild(a.cloneNode(true))
            }
            c.down("img").setOpacity(0.3)
        })
    },updateProgressEl: function(b) {
        var a = this.progressEl;
        a.value = b;
        if (b === 100) {
            setTimeout(function() {
                a.style.visibility = "hidden"
            }, 200)
        }
    },finish: function() {
        clearInterval(this.pollTimer);
        this.isInProgress = false;
        this.updateProgressEl(100);
        this.dashboard.updateSavedSurfacesData()
    }};
Dashboard.ChooseFontBtn = {init: function(b) {
        var a = $("choose-font-btn");
        if (a) {
            return new APE.widget.MenuButton(a, b.container, {toggleProperty: "display",toggleShowValue: "",toggleHideValue: "none"})
        }
    }};
Dashboard.ChooseFontMenu = {init: function(b, a) {
        this.fontSelector = b;
        this.options = a || {};
        if (!this.options.onFontsFetched) {
            this.options.onFontsFetched = function() {
            }
        }
        if (!this.options.onMenuItemChosen) {
            this.options.onMenuItemChosen = function() {
            }
        }
        this.chooseFontMenu = new Proto.Menu({className: "menu desktop font-menu",menuItems: this.buildChooseFontMenuItems(),zIndex: 1100});
        this.markEngOnlyItems();
        this.fetchFontDefinitions();
        return this.chooseFontMenu
    },markEngOnlyItems: function() {
        this.chooseFontMenu.list.select(".eng-only").each(function(a) {
            a.insert({after: '<span class="eng-only-info">' + Dashboard.prototype.localizedStrings.ENG_ONLY + "</span>"})
        })
    },fetchFontDefinitions: function() {
        var d = this;
        var a = $A(this.fontSelector.options).map(function(e) {
            return this.translateFontNameFromPretty(e.innerHTML)
        }, this);
        a = a.filter(function(e) {
            return e !== ""
        });
        var b = setInterval(function c() {
            if (a.length === 0) {
                clearInterval(b);
                d.options.onFontsFetched()
            }
            iterateOverFontNames: for (var g = 0, e = a.length; g < e; g++) {
                var h = a[g].toLowerCase(), f = Cufon.fonts[h];
                if (f) {
                    f.offsetLeft = __fontDefinitions[a[g]];
                    a.splice(g, 1);
                    break iterateOverFontNames
                }
            }
        }, 100)
    },translateFontNameFromPretty: function(a) {
        return a.replace(/\s/g, "_")
    },translateFontNameToPretty: function(a) {
        return a.replace(/_/g, " ")
    },createMenuItemCallback: function(a) {
        return (function() {
            var c = a.innerHTML, d = this.translateFontNameFromPretty(c);
            var b = $A(this.fontSelector.options).find(function(e) {
                return e.innerHTML === c
            });
            b.selected = true;
            this.options.onMenuItemChosen(d, a)
        }).bind(this)
    },createMenuItemClass: function(a) {
        return a.innerHTML.replace(/\s+/g, "_").toLowerCase() + (Element.hasClassName(a, "eng-only") ? " eng-only" : "")
    },buildChooseFontMenuItems: function() {
        var b = this;
        if (typeof Cufon !== "undefined") {
            Cufon.textOptions = {}
        }
        var a = $A(this.fontSelector.options).inject([], function(d, c) {
            if (c.value === "separator") {
                d.push({separator: true})
            } else {
                d.push({name: c.innerHTML,callback: b.createMenuItemCallback(c),className: b.createMenuItemClass(c)})
            }
            return d
        });
        return a
    }};
Dashboard.FontOptionsBtn = {init: function(b, d) {
        this.menuEl = b.container;
        if (!d) {
            d = function() {
            }
        }
        var a = $("font-options-btn");
        if (a) {
            var c = new APE.widget.MenuButton(a, b.container, {toggleProperty: "display",toggleShowValue: "",toggleHideValue: "none",onShow: this.onShow.bind(this, d)});
            c.disable();
            return c
        }
    },onShow: function(c) {
        var d = this.menuEl.down(".italic").up(), f = this.menuEl.down(".underline").up(), a = this.menuEl.down(".linethrough").up(), e = this.menuEl.down(".shadow").up(), b = c();
        if (!b) {
            return
        }
        e[b.textShadow ? "addClassName" : "removeClassName"]("selected");
        f[b.textDecoration === "underline" ? "addClassName" : "removeClassName"]("selected");
        a[b.textDecoration === "line-through" ? "addClassName" : "removeClassName"]("selected");
        d[b.fontStyle === "italic" ? "addClassName" : "removeClassName"]("selected")
    }};
Dashboard.FontOptionsMenu = {init: function(b) {
        this.options = b || {};
        if (!this.options.onMenuItemChosen) {
            this.options.onMenuItemChosen = function() {
            }
        }
        var c = this.buildMenuItems();
        var a = new Proto.Menu({className: "menu desktop font-options-menu",menuItems: c,zIndex: 1100});
        return a
    },makeCallback: function(b, a) {
        return function() {
            a.options.onMenuItemChosen(b)
        }
    },buildMenuItems: function() {
        return [{name: '<em class="italic">Курсив</em>',callback: this.makeCallback(function(a) {
                    a.fontStyle = a.fontStyle === "italic" ? "" : "italic"
                }, this)}, {name: '<span style="text-decoration:underline" class="underline">Подчеркнутый</span>',callback: this.makeCallback(function(a) {
                    a.textDecoration = a.textDecoration === "underline" ? "" : "underline"
                }, this)}, {name: '<del class="linethrough">Зачеркнутый</del>',callback: this.makeCallback(function(a) {
                    a.textDecoration = a.textDecoration === "line-through" ? "" : "line-through"
                }, this)}, {name: '<span style="text-shadow:rgb(0,0,0) 1px 1px 2px" class="shadow">С тенью</span>',title: "С тенью",callback: this.makeCallback(function(a) {
                    a.textShadow = a.textShadow === "#bbb 2px 2px 2px" ? "" : "#bbb 2px 2px 2px"
                }, this)}, {name: "В ВЕРХНИЙ РЕГИСТР",callback: this.makeCallback(function(a) {
                    a.text = a.text.toUpperCase()
                }, this)}, {name: "в нижний регистр",callback: this.makeCallback(function(a) {
                    a.text = a.text.toLowerCase()
                }, this)}]
    }};
Dashboard.LineHeightBtn = {init: function(c, b) {
        this.menuEl = c.container;
        if (!b) {
            b = function() {
            }
        }
        var d = $("line-height-btn");
        if (d) {
            var a = new APE.widget.MenuButton(d, c.container, {toggleProperty: "display",toggleShowValue: "",toggleHideValue: "none",onShow: this.onShow.bind(this, b)});
            a.disable();
            return a
        }
    },onShow: function(c) {
        var b = c();
        if (!b) {
            return
        }
        var a = this.menuEl.select("em").find(function(d) {
            return d.innerHTML === String(b.lineHeight)
        });
        this.menuEl.select("li a").invoke("removeClassName", "selected");
        if (a) {
            a.up().addClassName("selected")
        }
    }};
Dashboard.LineHeightMenu = {init: function(a) {
        this.options = a || {};
        if (!this.options.onMenuItemChosen) {
            this.options.onMenuItemChosen = function() {
            }
        }
        var b = this.buildMenuItems();
        var c = new Proto.Menu({className: "menu desktop line-height-menu",menuItems: b,zIndex: 1100});
        return c
    },makeCallback: function(b, a) {
        return function() {
            a.options.onMenuItemChosen(b)
        }
    },buildMenuItems: function() {
        var b = [];
        for (var a = -2; a <= 5; a += 0.5) {
            b.push({name: "<em>" + a + "</em>",callback: this.makeCallback((function(c) {
                    return function(d) {
                        d.lineHeight = c
                    }
                })(a), this),title: a})
        }
        return b
    }};
Dashboard.TextAlignBtn = {init: function(c, d) {
        if (!d) {
            d = function() {
            }
        }
        var b = $("text-align-btn");
        if (b) {
            var a = new APE.widget.MenuButton(b, c.container, {toggleProperty: "display",toggleShowValue: "",toggleHideValue: "none"});
            a.disable();
            return a
        }
    }};
Dashboard.TextAlignMenu = {init: function(b) {
        this.options = b || {};
        if (!this.options.onMenuItemChosen) {
            this.options.onMenuItemChosen = function() {
            }
        }
        var c = this.buildMenuItems();
        var a = new Proto.Menu({className: "menu desktop text-align-menu",menuItems: c,zIndex: 1100});
        return a
    },makeCallback: function(b, a) {
        return function() {
            a.options.onMenuItemChosen(b, this)
        }
    },buildMenuItems: function() {
        var a = '<span class="pseudo-icon left"><span class="line"></span><span class="line short"></span><span class="line"></span><span class="line short"></span></span>Влево';
        var c = '<span class="pseudo-icon center"><span class="line"></span><span class="line short"></span><span class="line"></span><span class="line short"></span></span>По центру';
        var b = '<span class="pseudo-icon right"><span class="line"></span><span class="line short"></span><span class="line"></span><span class="line short"></span></span>Вправо';
        return [{name: a,title: "Выровнить влево",callback: this.makeCallback(function(d) {
                    d.textAlign = "left"
                }, this)}, {name: c,title: "Выровнить по центру",callback: this.makeCallback(function(d) {
                    d.textAlign = "center"
                }, this)}, {name: b,title: "Выровнить вправо",callback: this.makeCallback(function(d) {
                    d.textAlign = "right"
                }, this)}]
    }};
(function() {
    if (typeof Dashboard === "undefined") {
        this.Dashboard = function() {
        }
    }
    var a = "/javascripts/fonts/#{fontFamily}.font.js";
    Object.extend(Dashboard.prototype, {FONT_PATH_TPL: a,localizedStrings: {CONTEXT_MENU_CLONE_TEXT: "Clone",CONTEXT_MENU_DELETION_TEXT: "Delete",CONTEXT_MENU_GROUP_DELETION_TEXT: "Delete this group",CONTEXT_MENU_OTHERS_DELETION_TEXT: "Delete others",CONTEXT_MENU_FLIP_VERTICALLY_TEXT: "Flip vertically",CONTEXT_MENU_FLIP_HORIZONTALLY_TEXT: "Flip horizontally",CONTEXT_MENU_SEND_BACKWARDS: "Send backwards",CONTEXT_MENU_SEND_TO_BACK: "Send to back",CONTEXT_MENU_BRING_FORWARD: "Bring forward",CONTEXT_MENU_BRING_TO_FRONT: "Bring to front",CONTEXT_MENU_CENTER_HORIZONTALLY: "Center horizontally",CONTEXT_MENU_CENTER_VERTICALLY: "Center vertically",CONTEXT_MENU_RESIZE_TO_FIT: "Resize to fit",PALETTE_PICKER_CELL_TITLE: "Hex code is - #{hexCode}",STRAIGHTEN_TEXT: "Straighten",SOMETHING_WRONG: "Something went wrong",SUCCESS_SAVED_TEXT: "Design was successfully saved",SAVING: "Saving...",LOADING: "Loading design...",EMPTY_MESSAGE: "Your project appears to be empty",CLEAR_CANVAS_CONFIRM_MESSAGE: "Clearing canvas won't let you undo your changes",LOAD_NEW_DESIGN_CONFIRM_MESSAGE: "Load new design? This will erase your current design"},COLORPICKER_IMAGES_PATH: "/images/colorpicker/",INITIAL_BG_COLOR: "#FFFFFF",CLONE_OFFSET: 10,GROUP_CLONE_OFFSET: 30,DISABLED_CONTROL_OPACITY: 0.4,THUMBNAIL_LOADING_OPACITY: 0.1,ERROR_COLOR: "#ff5555",SUCCESS_COLOR: "#008000",INITIAL_TEXT_COLOR_VALUE: "#000000",EXTENDED_WIDTH: 960,SCALE_MARGIN: 100,YUI_MARGIN_RIGHT: "24.0769em",STUB_ENABLED: false,ANIMATION_ENABLED: true,CAN_SET_BG_COLOR: true,DESIGN_ID: null,STATUS_MESSAGE_STYLES: {width: "12em",backgroundColor: "#555",backgroundPosition: "10px 50%"}})
})();
(function() {
    var b = 0.01;
    function a(c) {
        this.dashboard = c;
        this.productMeta = this.dashboard.productMeta;
        this.priceData = this.productMeta.price_data;
        this.itemPriceEl = $("item-price");
        this.totalPriceEl = $("total-price");
        this.itemPrice = parseInt(this.itemPriceEl.value, 10);
        this.surfaceStatuses = this.getSurfaceStatuses();
        document.observe("content:changed", this.onSurfaceChange.bind(this));
        document.observe("surface:design:added", this.makeHandlerForEvent("added"));
        document.observe("surface:design:removed", this.makeHandlerForEvent("removed"));
        if (this.priceData.pricing_scheme === "basic_variant_dependent") {
            document.observe("product:color:changed", this.onProductColorChange.bind(this))
        }
        if (this.priceData.pricing_scheme === "card_packs") {
            document.observe("color:selected", this.onSideColorChange.bind(this))
        }
        this.updatePriceView()
    }
    a.prototype = {onSideColorChange: function() {
            Dashboard.Surfaces.populate.call(this.dashboard);
            this.itemPrice = this.getPriceForCardPacksScheme();
            this.updatePriceView()
        },onProductColorChange: function(d) {
            var c = d.memo.curValue, f = this.getPriceForColor(c);
            if (this.hasDataOnBothSides()) {
                this.itemPrice = f.base_price + f.double_side_adjustment
            } else {
                this.itemPrice = f.base_price
            }
            this.updatePriceView()
        },getSurfaceStatuses: function() {
            return this.productMeta.designs.map(function(c) {
                return Dashboard.Surfaces.hasData(c.side)
            })
        },onSurfaceChange: function() {
            var e = this;
            this.dashboard.isDesignSaved = false;
            var d = this.getSurfaceStatuses();
            for (var c = this.surfaceStatuses.length; c--; ) {
                if (this.surfaceStatuses[c] !== d[c]) {
                    if (this.surfaceStatuses[c] === false) {
                        document.fire("surface:design:added", {index: c})
                    } else {
                        document.fire("surface:design:removed", {index: c})
                    }
                }
            }
            this.surfaceStatuses = d
        },makeHandlerForEvent: function(c) {
            var d = this;
            return function(f) {
                var g = d.priceData;
                if (g.pricing_scheme === "card_packs") {
                    d.calculatePriceForCardPacksScheme()
                } else {
                    if (g.pricing_scheme === "basic_variant_dependent") {
                        d.calculatePriceForBasicVariantDependentScheme(c)
                    } else {
                        if (g.pricing_scheme === "basic") {
                            d.calculatePriceForBasicScheme(c)
                        }
                    }
                }
                d.updatePriceView()
            }
        },getPriceForColor: function(e) {
            var d = this.priceData.variant_prices;
            for (var f = 0, c = d.length; f < c; f += 2) {
                if (d[f].color.indexOf(e) > -1) {
                    return d[f + 1]
                }
            }
        },getPriceForCardPacksScheme: function() {
            var c = this.priceData;
            if (this.hasDataOnBothSides() || this.hasBgOnBothSides()) {
                return c.price_map[this.productMeta.default_quantity] * c.double_side_factor
            }
            return c.price_map[this.productMeta.default_quantity]
        },calculatePriceForCardPacksScheme: function() {
            this.itemPrice = this.getPriceForCardPacksScheme()
        },calculatePriceForBasicScheme: function(c) {
            var d = this.priceData;
            if (c === "added") {
                if (this.hasDataOnBothSides()) {
                    this.itemPrice += d.double_side_adjustment
                }
            } else {
                if (c === "removed") {
                    if (this.hasDataOnEitherSide()) {
                        if (!this.hasDataOnBothSides()) {
                            this.itemPrice -= d.double_side_adjustment
                        }
                    }
                }
            }
        },calculatePriceForBasicVariantDependentScheme: function(c) {
            var d = this.getPriceForColor(this.dashboard.getCurrentColor());
            if (c === "added") {
                if (this.hasDataOnBothSides()) {
                    this.itemPrice += d.double_side_adjustment
                }
            } else {
                if (c === "removed") {
                    if (this.hasDataOnEitherSide()) {
                        if (!this.hasDataOnBothSides()) {
                            this.itemPrice -= d.double_side_adjustment
                        }
                    }
                }
            }
        },hasDataOnBothSides: function() {
            return Dashboard.Surfaces.hasData(0) && Dashboard.Surfaces.hasData(1)
        },hasDataOnEitherSide: function() {
            return Dashboard.Surfaces.hasData(0) || Dashboard.Surfaces.hasData(1)
        },hasBgOnBothSides: function() {
            var c = Dashboard.Surfaces.dataPlaceholders[0].value, f = Dashboard.Surfaces.dataPlaceholders[1].value;
            var d = c ? JSON.parse(c) : c, e = f ? JSON.parse(f) : f;
            return !!(d && d.background && !(/rgba\(\s*0,\s*0,\s*0,\s*0\s*\)/).test(d.background) && e && e.background && !(/rgba\(\s*0,\s*0,\s*0,\s*0\s*\)/).test(e.background))
        },updatePriceView: function() {
            if (!this.totalPriceEl) {
                return
            }
            var c = (this.itemPrice * b).toFixed(2);
            if (this.totalPriceEl.innerHTML !== c) {
                this.totalPriceEl.innerHTML = c
            }
        }};
    Dashboard.PriceController = a
})();
(function() {
    var a = {LOWERCASE_V: 118,UPPERCASE_V: 86,LOWERCASE_Z: 122,UPPERCASE_Z: 90,LOWERCASE_C: 99,UPPERCASE_C: 67};
    Dashboard.KeyObserver = {initialize: function(b) {
            this.dashboard = b;
            document.observe("keydown", this.onKeyDownPress.bind(this));
            document.observe("keypress", this.onKeyDownPress.bind(this))
        },onKeyDownPress: function(c) {
            var b = this.dashboard.getActive();
            if (!b) {
                return
            }
            var h = b.isType("group"), g = (c.shiftKey ? 10 : 1);
            if ((!b && !h && c.charCode !== a.LOWERCASE_Z && c.charCode !== a.LOWERCASE_V) || this.dashboard.textFocused) {
                return
            }
            if (c.type === "keydown") {
                switch (c.keyCode) {
                    case Event.KEY_LEFT:
                        c.stop();
                        b.saveState().set("left", b.get("left") - g).setCoords();
                        this.dashboard.history.add(new fabric.TransformCommand(b));
                        this.dashboard.activeObjectProps.update();
                        break;
                    case Event.KEY_RIGHT:
                        c.stop();
                        b.saveState().set("left", b.get("left") + g).setCoords();
                        this.dashboard.history.add(new fabric.TransformCommand(b));
                        this.dashboard.activeObjectProps.update();
                        break;
                    case Event.KEY_UP:
                        c.stop();
                        b.saveState().set("top", b.get("top") - g).setCoords();
                        this.dashboard.history.add(new fabric.TransformCommand(b));
                        this.dashboard.activeObjectProps.update();
                        break;
                    case Event.KEY_DOWN:
                        c.stop();
                        b.saveState().set("top", b.get("top") + g).setCoords();
                        this.dashboard.history.add(new fabric.TransformCommand(b));
                        this.dashboard.activeObjectProps.update();
                        break;
                    case Event.KEY_BACKSPACE:
                        c.stop();
                        c.preventObjectRemoval || this.dashboard.removeActive();
                        break;
                    case a.LOWERCASE_C:
                    case a.UPPERCASE_C:
                        if (c.ctrlKey || c.metaKey) {
                            this.objectToCopy = b
                        }
                        break;
                    case a.LOWERCASE_V:
                    case a.UPPERCASE_V:
                        if ((c.ctrlKey || c.metaKey) && this.objectToCopy) {
                            var f = (function(e) {
                                if (e.type === "group") {
                                    this.dashboard.cloneActiveGroup(e)
                                } else {
                                    this.dashboard.addObjectRandomly(e);
                                    this.dashboard.canvas.setActiveObject(e)
                                }
                            }).bind(this);
                            if (this.objectToCopy.type === "image") {
                                this.objectToCopy.clone(f)
                            } else {
                                f(this.objectToCopy.clone())
                            }
                        }
                        break
                }
            }
            switch (c.charCode) {
                case a.LOWERCASE_Z:
                    if (c.metaKey || c.ctrlKey) {
                        if (c.shiftKey) {
                            this.dashboard.history.forward()
                        } else {
                            this.dashboard.history.back()
                        }
                    }
                    break;
                case a.LOWERCASE_C:
                    var b = this.dashboard.canvas.getActiveObject();
                    if (b) {
                        this.objectToCopy = b
                    }
                    break;
                case a.LOWERCASE_V:
                    if (this.objectToCopy) {
                        var d = this.objectToCopy.clone();
                        this.dashboard.addObjectRandomly(d);
                        this.dashboard.canvas.setActiveObject(d)
                    }
                    break
            }
            this.dashboard.canvas.renderAll()
        }}
})();
(function() {
    var b = ".canvas-container", a = "menu desktop", c = 1100;
    Dashboard.ContextMenu = {initialize: function(d) {
            this.dashboard = d;
            this.onContextCenterHorizontally = this.makeHandler("centerObjectH");
            this.onContextCenterVertically = this.makeHandler("centerObjectV");
            this.dashboard.contextMenu = this.createMenu();
            this.removeBgFilter = new fabric.Image.filters.RemoveWhite({threshold: 100,distance: 20})
        },createMenu: function() {
            var d = this;
            return new Proto.Menu({selector: b,className: a,zIndex: c,menuItems: this.buildContextMenuItems(),beforeShow: function(f) {
                    return d.beforeContextMenuShow.call(this, f, d.dashboard)
                },beforeSelect: function(f) {
                    d.dashboard.menuBtn && d.dashboard.menuBtn.deactivate()
                }})
        },makeHandler: function(d) {
            var e = this;
            return function() {
                var f = e.dashboard.getActive();
                if (f) {
                    if (e.dashboard.ANIMATION_ENABLED) {
                        e.dashboard.canvas["fx" + (d.charAt(0).toUpperCase() + d.slice(1))](f)
                    } else {
                        e.dashboard.canvas[d](f);
                        e.dashboard.activeObjectProps.update()
                    }
                    f.setCoords();
                    e.dashboard.history.add(new fabric.TransformCommand(f))
                }
            }
        },beforeContextMenuShow: function(j, l) {
            var g = l.getActive();
            if (!g) {
                return false
            }
            var d = this.list.select("a"), o = d[0], n = d[13], i = d[14], k = d[15], h = d[16], f = h.up().previous();
            if (g.isType("group")) {
                o.title = l.localizedStrings.CONTEXT_MENU_GROUP_DELETION_TEXT;
                o.innerHTML = l.localizedStrings.CONTEXT_MENU_GROUP_DELETION_TEXT
            } else {
                o.title = l.localizedStrings.CONTEXT_MENU_DELETION_TEXT;
                o.innerHTML = l.localizedStrings.CONTEXT_MENU_DELETION_TEXT
            }
            var m = g.type === "group";
            n[m ? "hide" : "show"]();
            k[m ? "hide" : "show"]();
            i[m ? "hide" : "show"]();
            if (!m) {
                n.innerHTML = n.title = g.lockHorizontally ? l.localizedStrings.CONTEXT_MENU_UNLOCK_MOVEMENT : l.localizedStrings.CONTEXT_MENU_LOCK_MOVEMENT;
                k.innerHTML = k.title = g.lockRotation ? l.localizedStrings.CONTEXT_MENU_UNLOCK_ROTATION : l.localizedStrings.CONTEXT_MENU_LOCK_ROTATION;
                i.innerHTML = i.title = g.lockScaling ? l.localizedStrings.CONTEXT_MENU_UNLOCK_SCALING : l.localizedStrings.CONTEXT_MENU_LOCK_SCALING
            }
            if (g.type === "image") {
                h.style.display = "";
                f.style.display = "";
                h.innerHTML = h.title = (g.filters.length ? l.localizedStrings.RESTORE_BG : l.localizedStrings.REMOVE_BG)
            } else {
                f.style.display = "none";
                h.style.display = "none"
            }
            l.canvas._onMouseUp(j);
            return true
        },buildContextMenuItems: function() {
            var d = this.dashboard.localizedStrings;
            return [{name: d.CONTEXT_MENU_DELETION_TEXT,callback: this.onContextDelete.bind(this)}, {name: d.CONTEXT_MENU_OTHERS_DELETION_TEXT,callback: this.onContextDeleteOthers.bind(this)}, {separator: true}, {name: d.CONTEXT_MENU_CLONE_TEXT,callback: this.onContextClone.bind(this)}, {separator: true}, {name: d.CONTEXT_MENU_FLIP_VERTICALLY_TEXT,callback: this.onContextFlipVertically.bind(this)}, {name: d.CONTEXT_MENU_FLIP_HORIZONTALLY_TEXT,callback: this.onContextFlipHorizontally.bind(this)}, {separator: true}, {name: d.STRAIGHTEN_TEXT,callback: this.onContextStraighten.bind(this)}, {separator: true}, {name: d.CONTEXT_MENU_SEND_BACKWARDS,callback: this.onContextSendBackwards.bind(this)}, {name: d.CONTEXT_MENU_SEND_TO_BACK,callback: this.onContextSendToBack.bind(this)}, {name: d.CONTEXT_MENU_BRING_FORWARD,callback: this.onContextBringForward.bind(this)}, {name: d.CONTEXT_MENU_BRING_TO_FRONT,callback: this.onContextBringToFront.bind(this)}, {separator: true}, {name: d.CONTEXT_MENU_CENTER_HORIZONTALLY,callback: this.onContextCenterHorizontally}, {name: d.CONTEXT_MENU_CENTER_VERTICALLY,callback: this.onContextCenterVertically}, {name: d.CONTEXT_MENU_RESIZE_TO_FIT,callback: this.onContextResizeToFit.bind(this)}, {separator: true}, {name: d.CONTEXT_MENU_LOCK_MOVEMENT,callback: this.onContextLockMovement.bind(this)}, {name: d.CONTEXT_MENU_LOCK_SCALING,callback: this.onContextLockScaling.bind(this)}, {name: d.CONTEXT_MENU_LOCK_ROTATION,callback: this.onContextLockRotation.bind(this)}, {separator: true}, {name: d.REMOVE_BG,callback: this.onRemoveRestoreBg.bind(this)}]
        },onContextLockMovement: function() {
            var d = this.dashboard.getActive();
            if (d) {
                d.lockHorizontally = d.lockVertically = !d.lockHorizontally
            }
        },onContextLockScaling: function() {
            var d = this.dashboard.getActive();
            if (d) {
                d.lockScaling = !d.lockScaling
            }
        },onContextLockRotation: function() {
            var d = this.dashboard.getActive();
            if (d) {
                d.lockRotation = !d.lockRotation
            }
        },onContextDelete: function(d) {
            this.dashboard.removeActive()
        },onContextDeleteOthers: function() {
            this.dashboard.deleteAllExceptActive()
        },onContextClone: function() {
            this.dashboard.cloneActiveObject()
        },onContextFlipHorizontally: function() {
            this.dashboard.flipActiveObjectHorizontally()
        },onContextFlipVertically: function() {
            this.dashboard.flipActiveObjectVertically()
        },onContextResizeToFit: function() {
            this.dashboard.resizeActiveObjectToFit();
            this.onContextCenterHorizontally();
            this.onContextCenterVertically()
        },onContextStraighten: function() {
            this.dashboard.straightenActiveObject()
        },onContextSendToBack: function() {
            this.dashboard.sendActiveObjectToBack()
        },onContextBringToFront: function() {
            this.dashboard.bringActiveObjectToFront()
        },onContextSendBackwards: function() {
            this.dashboard.sendActiveObjectBackwards()
        },onContextBringForward: function() {
            this.dashboard.bringActiveObjectForward()
        },onRemoveRestoreBg: function() {
            var d = this.dashboard.getActive();
            if (!d) {
                return
            }
            d.filters = d.filters.length ? [] : [this.removeBgFilter];
            d.applyFilters(this.dashboard.canvas.renderAll.bind(this.dashboard.canvas))
        },renderAll: function() {
            this.dashboard.canvas.renderAll()
        }}
})();
(function() {
    var e = APE.dom, d = e.Event, c = d.getCoords, b = 5, a = -80;
    Dashboard.ThumbDragger = {initialize: function(f) {
            this.dashboard = f;
            APE.EventPublisher.add(this.dashboard.sidebarEl, "onmousedown", this.onMouseDown, this)
        },isValidTarget: function(f) {
            return ((e.isTagName(f, "img") && e.hasClass(f, "img")) || (e.isTagName(f, "a") && e.isTagName(f.parentNode, "li"))) && !Element.up(f, "li").hasClassName("polling")
        },initAddIcon: function() {
            if (!this._addIcon) {
                this._addIcon = new Image();
                this._addIcon.src = "/images/add.png";
                this._addIcon.style.position = "absolute";
                this._addIcon.style.zIndex = "5000";
                document.body.appendChild(this._addIcon)
            }
        },startShowingAddIcon: function() {
            this.boundAddIconMoveHandler = this.addIconMoveHandler.bind(this);
            Event.observe(document, "mousemove", this.boundAddIconMoveHandler);
            this.initAddIcon()
        },stopShowingAddIcon: function() {
            Event.stopObserving(document, "mousemove", this.boundAddIconMoveHandler);
            this._addIcon.style.display = "none"
        },addIconMoveHandler: function(h) {
            var g = c(h), f;
            f = this._addIcon.style;
            f.left = (g.x + b) + "px";
            f.top = (g.y + b) + "px";
            f.display = "";
            f = this.clone.style;
            f.left = (g.x + a) + "px";
            f.top = (g.y + a) + "px"
        },setCloneStyles: function(h, g, f) {
            h.style.position = "absolute";
            h.style.left = f.x + 1 + "px";
            h.style.top = f.y + 1 + "px";
            h.style.padding = e.getStyle(g, "padding");
            h.style.width = e.getStyle(g, "width");
            h.style.height = e.getStyle(g, "height");
            h.style.textAlign = e.getStyle(g, "textAlign");
            h.style.color = "#fff"
        },onDragStart: function(f) {
            this.hasMoved = true;
            this.startShowingAddIcon();
            document.fire("thumb:drag:started")
        },onDragEnd: function(n) {
            this.stopShowingAddIcon();
            var o = d.getCoords(n), m = this;
            if (this.clone && this.clone.parentNode) {
                this.clone.parentNode.removeChild(this.clone)
            }
            var i = this.dashboard.canvas.getElement(), k = e.getOffsetCoords(i), f = (k.x <= o.x && (k.x + i.offsetWidth) >= o.x && k.y <= o.y && (k.y + i.offsetHeight) >= o.y);
            if (f) {
                var g = this.clone.href, j = g.split("?")[0], h = this.clone.down("img").getAttribute("data-original-width"), l = this.clone.down("img").getAttribute("data-original-height");
                if (this.isImg) {
                    this.dashboard.loadImageFromURL(g, function(p) {
                        p.set("left", o.x - k.x).set("top", o.y - k.y);
                        m.dashboard.addObject(p);
                        m.dashboard.resizeObjectToFit(p, h, l)
                    })
                } else {
                    this.dashboard.loadSVGFromURL(g, function(r, q) {
                        var p = r[0];
                        p.setSourcePath(j);
                        if (r.length > 1) {
                            p = new fabric.PathGroup(r, q);
                            p.setSourcePath(j);
                            m.normalizeColor(p)
                        }
                        p.set("left", o.x - k.x).set("top", o.y - k.y);
                        m.dashboard.addObject(p);
                        m.dashboard.resizeObjectToFit(p);
                        m.dashboard.hideLoadingMessage()
                    })
                }
            }
            clone = drag = null
        },onMouseUp: function(g) {
            g = g || window.event;
            var l = this, f = g.target || g.srcElement;
            if (!f) {
                return
            }
            if (e.hasClass(f, "clone")) {
                f.parentNode.removeChild(f);
                f = this.target
            }
            if (e.hasClass(f, "img") || e.hasClass(f, "shape-thumb")) {
                if (e.hasClass(f, "img")) {
                    this.isImg = e.hasClass(f, "canvas-img");
                    f = f.parentNode
                }
                if (this.clone && this.clone.parentNode) {
                    this.clone.parentNode.removeChild(this.clone)
                }
                if (!this.hasMoved) {
                    var k = function(m) {
                        if (l.dashboard.isDraggingObject) {
                            return
                        }
                        l.dashboard.finishFreeDrawingMode();
                        l.dashboard.addObjectRandomly(m);
                        l.dashboard.resizeObjectToFit(m, j, h);
                        l.dashboard.canvas.renderAll()
                    };
                    if (this.isImg) {
                        var i = f.href, j = f.down("img").getAttribute("data-original-width"), h = f.down("img").getAttribute("data-original-height");
                        this.dashboard.loadImageFromURL(i, k)
                    } else {
                        this.shapesClickHandler(this.mouseDownEvent, k, this.target)
                    }
                }
            }
        },onMouseDown: function(i) {
            i = i || window.event;
            if (i.which !== 1) {
                return false
            }
            this.mouseDownEvent = i;
            d.preventDefault(i);
            var j = this.target = d.getTarget(i);
            if (j && this.isValidTarget(j)) {
                if (e.isTagName(j, "img")) {
                    j = j.parentNode
                }
                Element.extend(j);
                j.onclick = fabric.util.falseFunction;
                var n = j.down(".img");
                if (!n) {
                    return
                }
                var g = this.isImg = APE.dom.hasClass(n, "canvas-img"), m = e.getOffsetCoords(j), k = window.__clone = this.clone = j.cloneNode(true);
                k.className = "clone";
                k.id = j.id + "-dragged";
                this.setCloneStyles(k, j, m);
                var l = Element.down(k, ".img");
                l.style.width = e.getStyle(n, "width");
                l.style.padding = e.getStyle(n, "padding");
                document.body.appendChild(k);
                var h = APE.drag.Draggable.getByNode(k);
                this.hasMoved = false;
                document.documentElement.onmouseup = this.onMouseUp.bind(this);
                h.ondragstart = this.onDragStart.bind(this);
                h.ondragend = this.onDragEnd.bind(this);
                var f = d.getTarget;
                d.getTarget = function() {
                    return k
                };
                APE.drag.DragHandlers.mouseDown(i);
                d.getTarget = f
            }
        },normalizeColor: function(f) {
            if (f.isSameColor() && f.paths[0].fill !== f.fill) {
                f.fill = f.paths[0].fill
            }
        },shapesClickHandler: function(j, m, i) {
            var h;
            Event.extend(j);
            if (h = j.findElement("a")) {
                j.target && j.stop();
                var f = h.down(".img"), k = this.dashboard.spinnerEl;
                h.appendChild(k);
                if (!f) {
                    return
                }
                var g = h.href;
                var l = g.split("?")[0];
                f.setOpacity(this.THUMBNAIL_LOADING_OPACITY);
                k && k.show();
                this.dashboard.loadSVGFromURL(g, (function(p, o) {
                    var n = p[0];
                    n.setSourcePath(l);
                    if (p.length > 1) {
                        n = new fabric.PathGroup(p, o);
                        n.setSourcePath(l);
                        this.normalizeColor(n)
                    }
                    m(n);
                    k && k.hide();
                    f.setOpacity(1)
                }).bind(this))
            }
        }}
})();
fabric.Canvas.prototype.cloneWithoutData = function(c) {
    var a = fabric.document.createElement("canvas");
    a.width = this.getWidth();
    a.height = this.getHeight();
    var b = this.__clone || (this.__clone = new fabric.Canvas(a));
    b.clipTo = this.clipTo;
    if (c) {
        c(b)
    }
};
(function() {
    var a = {};
    a.create = function(i) {
        var f = a.currentIndex = 0, h = 1 / 6, b = i.canvas, c = !i.isBusinessCard();
        var g = $$(".alt-view-thumbnails")[0];
        if (g) {
            var d = g.select("li a .bg")
        }
        var e = $$("*[data-side-name]");
        if (g) {
            var j = g.select("li a img.design")
        }
        return Object.extend(a, {dataPlaceholders: e,setThumbSelected: function(k) {
                d[k].up().addClassName("selected")
            },updateThumb: function(l) {
                l = typeof l == "number" ? l : f;
                if (l === f) {
                    var k = i.toDatalessJSON();
                    if (e[l].value !== k) {
                        e[l].value = k;
                        a.updateDesign(l)
                    }
                }
            },updateDesign: function(k) {
                if (!j) {
                    return
                }
                setTimeout(function() {
                    j[k].src = b.toDataURLWithMultiplier("png", h);
                    j[k].style.display = ""
                }, 100)
            },updateAllThumbsExceptCurrent: function() {
                for (var l = 0, k = d.length; l < k; l++) {
                    if (l !== f) {
                        a.updateThumb(l)
                    }
                }
            },selectByIndex: function(k) {
                if (f !== k) {
                    d[f].up().removeClassName("selected");
                    a.save();
                    f = k;
                    a.currentIndex = k;
                    i.setCurrentSide(e[f].getAttribute("data-side-name"));
                    document.fire("surface:changed");
                    a.load(a.updateDesign.bind(a, k));
                    a.setThumbSelected(k)
                }
            },save: function() {
                e[f].value = i.toDatalessJSON()
            },load: function(m) {
                var k = b.backgroundColor, l = i.currentBgColor;
                if (e[f].value) {
                    i.loadFromDatalessJSON(e[f].value, m)
                } else {
                    i.loadFromDatalessJSON(fabric.Canvas.EMPTY_JSON, m)
                }
                i.setBgColor(l, true);
                if (c) {
                    b.backgroundColor = k
                }
                a.setCanvasDimensions()
            },setCanvasDimensions: function() {
                var l = i.getCurrentSideMetadata().dimensions;
                b.setDimensions({width: l[2],height: l[3]});
                var k = b.getElement().parentNode;
                k.style.top = l[0] + "px";
                k.style.left = l[1] + "px"
            },init: function() {
                this.loadDesignFromDefaultPlaceholder();
                this.prepareImagesPosition();
                var k = !!g;
                if (k) {
                    this.initEvents();
                    this.setThumbSelected(f);
                    this.updateAllThumbsExceptCurrent();
                    this.initSurfaces();
                    this.populateNonVisibleSidesDesigns()
                }
            },populateNonVisibleSidesDesigns: function() {
                i.productMeta.designs.each(function(k) {
                    if (k.side !== i.productMeta.default_side) {
                        var l = this.getPlaceholderByName(k.side);
                        i.canvas.cloneWithoutData(function(m) {
                            m.loadFromDatalessJSON(l.value, function() {
                                var n = $("preview-" + k.side).down("img.design");
                                n.src = m.toDataURLWithMultiplier("png", h);
                                n.show()
                            })
                        })
                    }
                }, this)
            },loadDesignFromDefaultPlaceholder: function() {
                var k = this.getPlaceholderByName(i.productMeta.default_side);
                this.loadDesignFromPlaceholder(k)
            },loadDesignFromPlaceholder: function(l) {
                if (l.value) {
                    try {
                        i.loadFromDatalessJSON(l.value, function() {
                            document.fire("design:loaded")
                        });
                        color = b.backgroundColor
                    } catch (k) {
                        console.log(k)
                    }
                }
            },initSurfaces: function() {
                this.surfaces = [];
                e.each(function(l, k) {
                    this.surfaces[k] = {price: i.getMetadataForSide(l.getAttribute("data-side-name")).price_adjustment * i.priceMultiplier}
                }, this)
            },initEvents: function() {
                document.observe(fabric.CommandHistory.CHANGE_EVENT, function() {
                    a.updateAllThumbsExceptCurrent();
                    a.updateThumb()
                });
                document.observe("color:changed", function(k) {
                    color = k.memo.color;
                    a.updateAllThumbsExceptCurrent();
                    a.updateThumb()
                });
                g.observe("click", (function(l, k) {
                    if (k = l.findElement("a")) {
                        l.stop();
                        if (!this.disabled) {
                            a.selectByIndex(d.indexOf(k.down("img")))
                        }
                    }
                }).bind(this))
            },hasData: function(k) {
                if (typeof k == "number") {
                    var l = e[k];
                    return !this.isJSONEmpty(l.value)
                } else {
                    if (typeof k === "string") {
                        l = this.getPlaceholderByName(k);
                        if (l) {
                            return !this.isJSONEmpty(l.value)
                        }
                    }
                }
                return e.any(function(m) {
                    return !this.isJSONEmpty(m.value)
                }, this)
            },populate: function() {
                a.origDataPlaceholders = a.origDataPlaceholders || [];
                a.save();
                a.replaceWithHashes();
                a.updateBackgrounds();
                a.updateDesign(f)
            },replaceWithHashes: function() {
                a.dataPlaceholders.each(function(l, k) {
                    a.origDataPlaceholders[k] = l.value;
                    l.value = i.replaceUrlsWithHashes(l.value)
                })
            },updateBackgrounds: function() {
                a.dataPlaceholders.each(function(m) {
                    if (m.value) {
                        if (c) {
                            var k = JSON.parse(m.value);
                            k.background = b.backgroundColor;
                            m.value = JSON.stringify(k)
                        }
                    } else {
                        var l = c ? b.backgroundColor : "rgba(0,0,0,0)";
                        m.value = '{"objects": [], "background": "' + l + '"}'
                    }
                })
            },disable: function() {
                this.disabled = true
            },enable: function() {
                this.disabled = false
            },size: function() {
                return this.dataPlaceholders.length
            },getByIndex: function(k) {
                return this.surfaces[k]
            },getPlaceholderByName: function(k) {
                return e.find(function(l) {
                    return l.getAttribute("data-side-name") === k
                })
            },populateWithOriginalData: function() {
                if (this.dataPlaceholders && this.origDataPlaceholders) {
                    this.dataPlaceholders.each(function(l, k) {
                        l.value = this.origDataPlaceholders[k]
                    }, this)
                }
            },isJSONEmpty: function(k) {
                return /^\s*\{\s*"objects"\s*:\s*\[\s*\]|^\s*$/.test(k)
            },getAllData: function() {
                return this.dataPlaceholders.reduce(function(k, l) {
                    return k += l.value
                }, "")
            },prepareImagesPosition: function() {
                if (!j) {
                    return
                }
                j.each(function(l, k) {
                    var m = i.productMeta.designs[k].dimensions;
                    l.style.top = (m[0] * h).toFixed(2) + "px";
                    l.style.left = (m[1] * h).toFixed(2) + "px";
                    l.style.width = (m[2] * h).toFixed(2) + "px";
                    l.style.height = (m[3] * h).toFixed(2) + "px"
                })
            }})
    };
    if (typeof Dashboard !== "undefined") {
        Dashboard.Surfaces = a
    }
})();
Dashboard.Uploader = (function() {
    var h, a = $$(".uploader")[0], f = 'html { border: 0; background: transparent; }body, form { margin: 0; background: transparent; font-family: "Helvetica Neue", "Helvetica", "Trebuchet MS", sans-serif; }form { margin: 0 0 0 0.5em; }form p span { margin-right: 0.25em; font-size: 1.2em; position: relative; top: 2px; font-family: Georgia, serif; }form .upload-wrapper { margin-bottom: 0.5em; }form .submit-wrapper { margin-bottom: 0; margin-top: 0; }.info-box { border-bottom: 1px dotted blue; padding: 0 3px; position: relative; display: inline-block; }.info-box .tooltip { display: none; position: absolute; left: -19px; top: 27px; text-decoration: none; background: #fff; width: 280px; color: #333; padding: 0.75em; border-radius: 4px; z-index: 1000; text-align: left; }.info-box:hover .tooltip, .info-box:focus .tooltip { display: inline-block; }';
    function i() {
        if (!a) {
            return
        }
        var n = l();
        a.insert(n);
        h = $$("iframe")[0].contentWindow;
        c()
    }
    function l() {
        var n = document.createElement("iframe");
        n.style.cssText = "border:0;background:transparent;height:85px";
        n.frameBorder = n.scrolling = "no";
        n.allowTransparency = true;
        n.src = "";
        return n
    }
    function c() {
        j(h.document);
        var n = setInterval(function() {
            if (h.document.body) {
                clearInterval(n);
                document.fire("iframe:loaded")
            }
        }, 100);
        document.observe("iframe:loaded", g)
    }
    function j(q) {
        var n = Dashboard.prototype.localizedStrings, p = n.SAVE_IMG;
        var o = ["<html><head><style>", f, "</style></head><body>", '<form method="post" id="new_image" enctype="multipart/form-data" class="clearfix" action="/images.json">', "<div>", '<p class="upload-wrapper"><span>1.</span><input type="file" size="10" name="image[picture]" id="image_picture" multiple="multiple"></p>', '<p class="submit-wrapper"><span>2.</span><input type="submit" value="' + p + '" name="commit" id="image_submit" class="submit-form"></p>', "</div>", "</form></body></html>"].join("");
        q.write(o);
        q.close()
    }
    var e = $$(".add-image ul")[0];
    function m() {
        if (e) {
            e.insert({top: Dashboard.Uploader.getImageThumbMarkup().interpolate({small_url: "/images/spinner.gif",id: "#{id}",original_width: "#{original_width}",original_height: "#{original_height}",})});
            var n = e.down("li");
            n.down(".remove").hide();
            n.addClassName("polling");
            return n
        }
    }
    function b(n, o) {
        n.down("a").href = o.canvas_url;
        n.down('[id="image-#{id}"]').id = o.id;
        n.down('[src*="spinner.gif"]').src = o.small_url;
        n.down("[data-original-width]").setAttribute("data-original-width", o.original_width);
        n.down("[data-original-height]").setAttribute("data-original-height", o.original_height);
        n.down(".remove").show();
        n.down(".info").hide();
        n.removeClassName("polling")
    }
    function g() {
        var n = h.document.forms[0];
        if (n) {
            n.onsubmit = k
        }
    }
    function d(n, p) {
        var q = 1000;
        var o = setInterval(function() {
            new Ajax.Request(n, {method: "GET",onSuccess: function(t) {
                    if (!o) {
                        return
                    }
                    var s = t.responseJSON;
                    if (s && s.image) {
                        var u = s.image;
                        clearInterval(o);
                        b(p, u)
                    }
                }})
        }, q)
    }
    function k() {
        var o = this.elements["image[picture]"];
        var r = (typeof o.files !== "undefined" && typeof o.files.length === "number");
        var s = r ? (o.files.length > 0) : (o.value !== "");
        if (!s) {
            return false
        }
        var n = this.elements.commit;
        n.value = Dashboard.prototype.localizedStrings.SAVING;
        n.disabled = true;
        var p = h.document.body.innerHTML;
        var q = setInterval(function() {
            try {
                if (!h.document || !h.document.body || !h.document.body.innerHTML) {
                    return
                }
            } catch (v) {
                clearInterval(q);
                a.down("iframe").remove();
                i()
            }
            var y = h.document.body.innerHTML;
            if (y !== p) {
                clearInterval(q);
                var x = h.document.getElementsByTagName("pre")[0];
                if (!x) {
                    a.down("iframe").remove();
                    i();
                    return
                }
                var u = JSON.parse(x.innerHTML);
                if (typeof u == "string") {
                    var w = m();
                    d(u, w)
                } else {
                    if (u.length) {
                        var t = u[0];
                        alert(t[0] + " " + t[1])
                    }
                }
                a.down("iframe").remove();
                i()
            }
        }, 100)
    }
    $$("[data-poll-url]").each(function(n) {
        d(n.getAttribute("data-poll-url"), n)
    });
    return {initialize: i,getImageThumbMarkup: function() {
            return ["<li>", '<a title="" class="shape-thumb" id="image-#{id}">', '<span class="remove"></span>', '<img class="canvas-img img" src="#{small_url}" alt="" data-original-width="#{original_width}" data-original-height="#{original_height}">', '<span class="info">', Dashboard.prototype.localizedStrings.PLEASE_WAIT, "</span>", "</a>", "</li>"].join("")
        }}
})();
Dashboard.DragDropUploader = (function() {
    var b = $$(".add-image .list-wrapper ul")[0];
    function g() {
        window.ondragover = f;
        window.ondragleave = h;
        window.ondrop = c
    }
    function f(i) {
        i.preventDefault && i.preventDefault();
        b.addClassName("over")
    }
    function h() {
        b.removeClassName("over")
    }
    function c(j) {
        j.preventDefault && j.preventDefault();
        var i = j.dataTransfer && j.dataTransfer.files && j.dataTransfer.files[0];
        e(i);
        b.removeClassName("over");
        return false
    }
    function e(i) {
        if (typeof FormData == "undefined") {
            return
        }
        if (i && i.type && i.type.match(/^image\/.*/)) {
            var j = new FormData();
            j.append("image[picture]", i);
            d(j)
        }
    }
    function d(i) {
        var j = new XMLHttpRequest();
        j.open("POST", "/images.json");
        j.onreadystatechange = function(k) {
            if (j.readyState === 4) {
                if (j.status > 200 && j.status < 400) {
                    a(j.responseText)
                } else {
                    console.log("error")
                }
            }
        };
        j.send(i)
    }
    function a(j) {
        var i = JSON.parse(j);
        if (i && i.image) {
            b.insert({top: Dashboard.Uploader.getImageThumbMarkup().interpolate(i.image)})
        }
    }
    return {initialize: function() {
            if (!b) {
                return
            }
            g()
        }}
})();
(function() {
    if (typeof Dashboard === "undefined") {
        console.warn("Dashboard does not exist. Can't proceed.");
        return
    }
    Dashboard.TextPlaceholder = Class.create({initialize: function(a) {
            this.element = $(a);
            if (this.element) {
                this.element.style.color = Dashboard.TextPlaceholder.INACTIVE_COLOR;
                this._addEventHandlers()
            }
        },_addEventHandlers: function() {
            this._addFocusHandler();
            this._addKeyDownHandler();
            this._addKeyUpHandler();
            this._addBlurHandler()
        },_addFocusHandler: function() {
            var a = this;
            APE.EventPublisher.add(this.element, "onfocus", (this._onFocus = function() {
                if (this.value === this.title) {
                    a.activate()
                }
            }))
        },_addKeyDownHandler: function() {
            APE.EventPublisher.add(this.element, "onkeydown", (this._onKeyDown = function(a) {
                a = a || window.event;
                if (a.keyCode === Event.KEY_RETURN) {
                }
            }))
        },_addKeyUpHandler: function() {
            this.lastValue = this.element.value;
            APE.EventPublisher.add(this.element, "onkeyup", (this._onKeyUp = function(b) {
                b = b || window.event;
                var a = APE.dom.Event.getTarget(b);
                if (a.value !== this.lastValue) {
                    this.fire("value:changed", {value: a.value});
                    this.lastValue = a.value
                }
            }), this);
            APE.EventPublisher.add(this.element, "input", this._onKeyUp, this)
        },_addBlurHandler: function() {
            var a = this;
            APE.EventPublisher.add(this.element, "onblur", (this._onBlur = function() {
                if (this.value === "") {
                    a.fire("value:removed")
                }
                if (this.value.blank()) {
                    a.deactivate()
                }
            }))
        },activate: function() {
            this.element.style.color = Dashboard.TextPlaceholder.ACTIVE_COLOR;
            this.element.value = ""
        },deactivate: function() {
            this.element.style.color = Dashboard.TextPlaceholder.INACTIVE_COLOR;
            this.element.value = this.element.title
        },enable: function() {
            this.element.enable()
        },disable: function() {
            this.element.disable()
        },fire: function() {
            return this.element.fire.apply(this.element, arguments)
        },observe: function() {
            return this.element.observe.apply(this.element, arguments)
        },getValue: function() {
            return this.element.value
        },setValue: function(a) {
            this.element.value = a
        },dispose: function() {
            APE.EventPublisher.remove(this.element, "onfocus", this._onFocus);
            APE.EventPublisher.remove(this.element, "onkeydown", this._onKeyDown);
            APE.EventPublisher.remove(this.element, "onkeyup", this._onKeyUp, this);
            APE.EventPublisher.remove(this.element, "onblur", this._onBlur)
        }});
    Dashboard.TextPlaceholder.ACTIVE_COLOR = "#000000";
    Dashboard.TextPlaceholder.INACTIVE_COLOR = "#888888"
})();
Dashboard.TextPopup = Class.create({initialize: function(b, a) {
        this.element = $(b);
        this.textareaEl = this.element.down("textarea");
        this.wrapperEl = this.element.down(".wrapper");
        this.togglerEl = this.element.down(".toggler");
        this.initObservers();
        this.makeDraggable()
    },initObservers: function() {
        var a = this;
        this.element.down(".toggler").onclick = function() {
            a.toggleCollapsing();
            return false
        }
    },makeDraggable: function() {
        var a = APE.drag.Draggable.getByNode(this.element);
        a.keepInContainer = true;
        a.setHandle(this.element.down(".header"))
    },show: function() {
        this.element.show()
    },hide: function() {
        this.element.hide()
    },toggleCollapsing: function() {
        if (this.wrapperEl.visible()) {
            this.collapse();
            this.togglerEl.innerHTML = "&darr;"
        } else {
            this.expand();
            this.togglerEl.innerHTML = "&uarr;"
        }
    },collapse: function() {
        this.wrapperEl.hide()
    },expand: function() {
        this.wrapperEl.show()
    },selectText: function() {
        this.textareaEl.select()
    }});
Dashboard.ConfirmDialog = {prompt: function(b, f) {
        var e = document.getElementById("page-shim"), d = document.getElementById("confirm-dialog"), a = Dashboard.prototype.localizedStrings.IN_PLACE_EDITOR_CANCEL_TEXT, c = Dashboard.prototype.localizedStrings.IN_PLACE_EDITOR_OK_TEXT;
        if (!e) {
            e = document.createElement("div");
            e.id = "page-shim";
            e.style.cssText = "position:fixed;left:0;top:0;width:100%;height:100%;background:rgba(0,0,0,0.7);z-index:1000;display:none;";
            document.body.appendChild(e)
        }
        if (!d) {
            d = document.createElement("div");
            d.id = "confirm-dialog";
            d.style.cssText = "position:fixed;left:50%;top:0;width:30em;background:#fff;z-index:1100;margin-left:-15em;display:none;-moz-box-shadow: 0 5px 10px rgba(0, 0, 0, 0.3);";
            d.innerHTML = '<p style="font-size:1.5em;margin-top:0.5em;" class="message">&nbsp;</p><p><button type="button" class="ok" style="margin-right:0.5em;">OK</button><button type="button" class="cancel">' + a + "</button></p>";
            d.observe("click", function(h, g) {
                if ((g = h.findElement(".ok")) || (g = h.findElement(".cancel"))) {
                    d.hide();
                    e.hide();
                    f(g.hasClassName("ok"))
                }
            });
            document.body.appendChild(d)
        }
        d.down(".message").innerHTML = b;
        e.show();
        d.show()
    }};
(function(b) {
    var f = b.window, d, l = f.localStorage, g = (function() {
        try {
            return f.sessionStorage
        } catch (m) {
        }
    })();
    var a = {};
    var j = (function() {
        var x = "Printio", m = "1.0", o = "Printio", w = 10 * 1024 * 1024, u = "SVGShapes";
        var y = p();
        if (!y) {
            return
        }
        v();
        return ({save: q,load: t,has: s,clear: n});
        function p() {
            if (!f.openDatabase) {
                return
            }
            try {
                return f.openDatabase(x, m, o, w)
            } catch (z) {
                if (console && console.warn) {
                    console.warn("Error while opening database. " + z)
                }
            }
        }
        function v() {
            y.transaction(function(z) {
                z.executeSql("SELECT COUNT(*) FROM " + u, [], function() {
                }, r)
            })
        }
        function r(z, A) {
            z.executeSql("CREATE TABLE " + u + " (name TEXT UNIQUE, data TEXT)", [], function() {
            })
        }
        function q(z, A, B) {
            y.transaction(function(C) {
                C.executeSql("REPLACE INTO " + u + " (name, data) VALUES ('" + z + "', '" + JSON.stringify(A) + "')", [], B)
            });
            return A
        }
        function t(z, B) {
            function A(C, D) {
                var E = D.rows.item(0);
                if (!E) {
                    return B()
                }
                B(typeof E.data == "string" ? JSON.parse(E.data) : d)
            }
            y.transaction(function(C) {
                C.executeSql("SELECT data FROM " + u + " WHERE name = '" + z + "'", [], A)
            })
        }
        function s(z, B) {
            function A(C, D) {
                B(D.rows.length > 0)
            }
            y.transaction(function(C) {
                C.executeSql("SELECT data FROM " + u + " WHERE name = '" + z + "'", [], A)
            })
        }
        function n() {
            y.transaction(function(z) {
                z.executeSql("DELETE FROM " + u)
            })
        }
    })();
    function c(m, n) {
        n(a[m])
    }
    if (l) {
        c = function(m, o) {
            var n = l.getItem(m);
            o(typeof n == "string" ? JSON.parse(n) : d)
        }
    } else {
        if (g) {
            c = function(m, o) {
                var n = g.getItem(m);
                o((n && typeof n.value == "string") ? JSON.parse(n.value) : d)
            }
        } else {
            if (j) {
                c = function(m, n) {
                    j.load(m, function(o) {
                        n(o)
                    })
                }
            }
        }
    }
    function h(m, n) {
        return (a[m] = n)
    }
    if (l || g) {
        h = function(m, n) {
            (l || g).setItem(m, JSON.stringify(n));
            return n
        }
    } else {
        if (j) {
            h = j.save
        }
    }
    function i(m, n) {
        n(m in a)
    }
    if (l || g) {
        i = function(m, n) {
            n((l || g).getItem(m) !== null)
        }
    } else {
        if (j) {
            i = j.has
        }
    }
    function e() {
        for (var m in a) {
            delete a[m]
        }
    }
    if (l && l.clear) {
        e = function() {
            l.clear()
        }
    } else {
        if (g) {
            e = function() {
                for (var m in g) {
                    g.removeItem(m)
                }
            }
        } else {
            if (j) {
                e = j.clear
            }
        }
    }
    var k = b.Dashboard || (b.Dashboard = {});
    k.SVGCache = ({get: c,set: h,has: i,clear: e,isPersistent: !!(l || g || j)})
})(this);
(function(d, f) {
    var c = document.body;
    if (c) {
        var g = c.firstChild;
        var e = $("cp1_Container");
        if (e) {
            c.insertBefore(e, g)
        }
        var a = $("active-opacity-container");
        if (a) {
            c.insertBefore(a, g)
        }
    }
})();
(function() {
    var a = this.Dashboard;
    if (!a || !a.prototype || !a.prototype.localizedStrings) {
        window.setTimeout(arguments.callee, 100);
        return
    }
    Object.extend(a.prototype.localizedStrings, {CONTEXT_MENU_CLONE_TEXT: "Клонировать",CONTEXT_MENU_DELETION_TEXT: "Удалить",CONTEXT_MENU_GROUP_DELETION_TEXT: "Удалить группу",CONTEXT_MENU_OTHERS_DELETION_TEXT: "Удалить все кроме выбранного",CONTEXT_MENU_FLIP_VERTICALLY_TEXT: "Перевернуть вертикально",CONTEXT_MENU_FLIP_HORIZONTALLY_TEXT: "Перевернуть горизонтально",STRAIGHTEN_TEXT: "Выровнять",SOMETHING_WRONG: "Неполадочка вышла",SAVING: "Сохраняется...",SAVE: "Сохранить",SAVE_IMG: "Закачать картинку",REMOVE_BACKGROUND: '<span class="translation_missing">ru, localized_js, remove_background</span>',REMOVE_BACKGROUND_INFO: '<span class="translation_missing">ru, localized_js, remove_background_info</span>',LOADING: "Загружается...",SUCCESS_SAVED_TEXT: "Успешно сохранено",CONTEXT_MENU_SEND_BACKWARDS: "На второй план",CONTEXT_MENU_SEND_TO_BACK: "На задний план",CONTEXT_MENU_BRING_FORWARD: "На первый план",CONTEXT_MENU_BRING_TO_FRONT: "На передний план",CONTEXT_MENU_CENTER_HORIZONTALLY: "Центрировать по горизонтали",CONTEXT_MENU_CENTER_VERTICALLY: "Центрировать по вертикали",CONTEXT_MENU_RESIZE_TO_FIT: "Масштабировать",CONTEXT_MENU_LOCK_MOVEMENT: "Заблокировать движение",CONTEXT_MENU_LOCK_ROTATION: "Заблокировать поворачивание",CONTEXT_MENU_LOCK_SCALING: "Заблокировать масштабирование",CONTEXT_MENU_UNLOCK_MOVEMENT: "Разблокировать движение",CONTEXT_MENU_UNLOCK_ROTATION: "Разблокировать поворачивание",CONTEXT_MENU_UNLOCK_SCALING: "Разблокировать масштабирование",PALETTE_PICKER_CELL_TITLE: "Код цвета (hex) - #{hexCode}",EMPTY_MESSAGE: "Ваш проект выглядит пустым",CLEAR_CANVAS_CONFIRM_MESSAGE: "Очистка не позволит вам отменить изменения. Продолжить?",LOAD_NEW_DESIGN_CONFIRM_MESSAGE: "Загрузить новый дизайн? Это действие сотрёт все изменения.",UNNAMED: "Не назван",DESIGN_TITLE_ALT_TEXT: "Щёлкните мышкой для редактирования",UPLOADER_DELETE_TEXT: "Удалить",UPLOADER_DELETE_TITLE_TEXT: "Удалить файл из очереди для закачивания",UPLOADER_SIZE_ERROR_TEXT: "ВНИМАНИЕ - Размер вашего файла - {fileSize} - превышает максимально допустимые {maxSize}",UPLOADER_TYPE_ERROR_TEXT: "ВНИМАНИЕ - Ваш файл ({fileName}) имеет неправильное разрешение - {fileType}",IN_PLACE_EDITOR_OK_TEXT: "Сохранить",IN_PLACE_EDITOR_CANCEL_TEXT: "Отменить",LEAVE_PAGE_WARNING: "Покидая эту страницу, вы потеряете несохранённые изменения",ENG_ONLY: "(Только Англ.)",PLEASE_WAIT: "Пожалуйста,<br>подождите.",REMOVE_BG: "Удалить белый фон",RESTORE_BG: "Восстановить белый фон"})
})();
var Progressify = (function() {
    function b(d, c) {
        this.duration = d;
        this.progress = c
    }
    function a() {
        this.points = [];
        this.prevProgress = 0
    }
    a.prototype.STARTING_SLOPE = 4;
    a.prototype.addPoint = function(d, c) {
        if (this.actualProgressIncreased(c)) {
            this.points.push(new b(d, c))
        }
    };
    a.prototype.progressAt = function(c) {
        var d;
        if (this.points.length !== 0 && this.points[this.points.length - 1].progress >= 100) {
            d = 100
        } else {
            d = this.calcProgress(c)
        }
        d = Math.max(this.prevProgress, Math.round(d));
        this.prevProgress = d;
        return d
    };
    a.prototype.calcProgress = function(c) {
        return Math.min(99, (c * this.averageSlope()))
    };
    a.prototype.actualProgressIncreased = function(d) {
        var c = this.points[this.points.length - 1] ? this.points[this.points.length - 1].progress : 0;
        return d > c
    };
    a.prototype.slopes = function() {
        var h = [this.STARTING_SLOPE];
        for (var g = 0, e = this.points.length; g < e; g++) {
            var d = this.points[g];
            var f = (g == 0 ? 0 : this.points[g - 1].duration);
            var c = (g == 0 ? 0 : this.points[g - 1].progress);
            h.push((d.progress - c) / (d.duration - f))
        }
        return h
    };
    a.prototype.averageSlope = function() {
        var f = 0;
        for (var d = 0, e = this.slopes(), c = e.length; d < c; d++) {
            f += e[d]
        }
        return f / e.length
    };
    return a
})();
(function() {
    var b = (function(f) {
        var e = f.style;
        return (typeof e.transform === "string" || typeof e.MozTransform === "string" || typeof e.WebkitTransform === "string" || typeof e.OTransform === "string")
    })(document.documentElement);
    if (b) {
        document.documentElement.className += " supports-transform"
    }
    Element.addMethods({getDataAttribute: function(f, e) {
            return f.getAttribute("data-" + e)
        }});
    function a() {
        if (!document.getElementById("url-for-fb-like-button")) {
            return
        }
        var f = document.createElement("iframe"), e = document.getElementById("url-for-fb-like-button").value;
        f.scrolling = "no";
        f.frameborder = "0";
        f.style.cssText = "border:none;height:70px;margin-top:10px;width:420px";
        f.allowTransparency = "true";
        f.src = "http://www.facebook.com/plugins/like.php?locale=ru_RU&href=" + e + "&amp;layout=button_count&amp;show_faces=false&amp;width=450&amp;action=like&amp;colorscheme=light&amp;height=70";
        $("vkontakte-btn").insert({after: f})
    }
    function c() {
        $$(".extra-description").invoke("hide").each(function(e) {
            e.previous(".extra-description-toggler").show()
        })
    }
    function d() {
        var e = document.getElementById("vkontakte-btn");
        if (!e) {
            return
        }
        e.innerHTML = VK.Share.button({url: document.location.href,title: document.title,description: $$('meta[name="description"]')[0].content,image: $("permalink-field").value + ".jpg",noparse: false})
    }
    document.observe("dom:loaded", function() {
        $$(".operator").each(function(y) {
            y.innerHTML = "="
        });
        a();
        d();
        c();
        e();
        var f = $$(".thumb img.background"), k = $$(".view"), j, u = [], s;
        function t(B) {
            if (--j !== 0) {
                return
            }
            $$(".spinner").each(function(C) {
                C.up().down("a").style.visibility = "visible";
                C.remove()
            });
            for (var y = u.length; y--; ) {
                var z = u[y].retrieve("src");
                if (z) {
                    u[y].src = z
                } else {
                    var A = u[y].retrieve("backgroundImage");
                    if (A) {
                        if (typeof u[y].src == "string") {
                            u[y].src = A
                        } else {
                            u[y].style.backgroundImage = "url(" + A + ")"
                        }
                    }
                }
            }
            n(B)
        }
        function l(A) {
            j = f.length + k.length;
            u = [];
            var L = new Element("img", {src: "/images/spinner.gif",className: "spinner"});
            L.setStyle({position: "absolute",left: "-1px",top: "-3px"});
            var F = A.up();
            F.setStyle({position: "relative"});
            F.appendChild(L);
            s = F;
            A.style.visibility = "hidden";
            var Q = A.getDataAttribute("color-name"), T = A.up("form").down(".product-quantity");
            for (var P = k.length; P--; ) {
                var V = k[P].getDataAttribute("bg-url"), y = k[P].getDataAttribute("bg-large-url"), U = k[P].down(".overlay") && k[P].down(".overlay").src, z = k[P].down(".large-preview .overlay") && k[P].down(".large-preview .overlay").src, K = /(.*\/)([^\/]*)(\..*)$/;
                console.log(z);
                if (V && y) {
                    var H = V.replace(K, "$1" + Q + "$3"), G = y.replace(K, "$1" + Q + "$3"), J = U && U.replace(K, "$1" + Q + "$3"), R = z && z.replace(K, "$1" + Q + "$3"), B = k[P].down(".bg-image"), N = k[P].down(".large-preview img"), C = k[P].down(".overlay"), S = k[P].down(".large-preview .overlay"), D = new Image(), I = new Image();
                    D.onload = (function(W) {
                        return function() {
                            t(W)
                        }
                    })(T);
                    D.src = H;
                    I.onload = (function(W) {
                        return function() {
                            t(W)
                        }
                    })(T);
                    I.src = G;
                    B.store("src", H);
                    N.store("src", G);
                    if (C) {
                        C.store("src", J);
                        S.store("src", R)
                    }
                    u.push(B, N);
                    if (C) {
                        u.push(C, S)
                    }
                }
            }
            for (var P = f.length; P--; ) {
                if (f[P]) {
                    V = f[P].up().getDataAttribute("bg-url");
                    var E = f[P].up().down(".overlay");
                    U = E && E.src;
                    if (V) {
                        H = V.replace(/(.*\/)([^\/]*)(\..*)$/, "$1" + Q + "$3");
                        J = U && U.replace(/(.*\/)([^\/]*)(\..*)$/, "$1" + Q + "$3");
                        var M = new Image();
                        M.onload = (function(W) {
                            return function() {
                                t(W)
                            }
                        })(T);
                        M.src = H;
                        f[P].store("backgroundImage", H);
                        if (E) {
                            E.store("backgroundImage", J)
                        }
                        u.push(f[P]);
                        if (E) {
                            u.push(E)
                        }
                    }
                }
            }
            var O = A.up(1).select(".selected")[0];
            if (O) {
                O.removeClassName("selected")
            }
            A.addClassName("selected")
        }
        function o(z) {
            var A = (/thumb-(.*)$/.exec(z.id) || {})[1];
            if (typeof A != "undefined") {
                $$(".view").invoke("hide");
                var y = $("view-" + A);
                y.show();
                document.fire("design:visibility:changed", {visibleElement: y})
            }
        }
        function h(y) {
            y.up("ul").select("a").invoke("removeClassName", "selected");
            y.addClassName("selected");
            n(y.up("form").down(".product-quantity"))
        }
        function v(y) {
            l(y)
        }
        function m(A) {
            var y = A.next(".extra-description"), B = 500;
            if (y.visible()) {
                emile(y, "opacity:0", {duration: B,after: function() {
                        y.setOpacity(1).hide()
                    }})
            } else {
                y.setOpacity(0).show();
                emile(y, "opacity:1", {duration: B})
            }
            var C = A.firstChild.nodeValue, z = C.indexOf("\u2191") > -1 ? C.replace(/\u2191/, "\u2193") : C.replace(/\u2193/, "\u2191");
            A.firstChild.nodeValue = z
        }
        function i(B) {
            var C = $("product-permalink-link"), z = $("permalink-field");
            if (!C) {
                return
            }
            var A = decodeURIComponent(C.href), y = A.replace(/(present\[variant\]=)([^&$]+)/, "$1" + B);
            C.href = y;
            z.value = y
        }
        function g() {
            var y = $$("[data-option-name]").inject([], function(z, B) {
                var A = B.getAttribute("data-option-name"), C = B.down("[data-" + A + "-name].selected").getAttribute("data-" + A + "-name");
                z.push(A + ":" + C);
                return z
            }).join("|");
            $("line_item_variant").value = y;
            i(y)
        }
        function w() {
            return $("line_item_variant").value
        }
        function q(A) {
            var z = parseInt(A.value, 10), y = parseInt(A.getAttribute("data-default-quantity"), 10), B = (Math.floor(z / y) === z / y) && z !== 0;
            A.style.backgroundColor = B ? "" : "#fcc"
        }
        function n(z) {
            var y = document.location.href.replace(document.location.search, "") + "/price";
            new Ajax.Request(y + "?quantity=" + z.value + "&variant=" + w(), {method: "get",onSuccess: function(A) {
                    var B = $$(".product-price")[0];
                    B.innerHTML = A.responseText;
                    B.style.backgroundColor = "#ffa";
                    emile(B, "background-color:#fff", {after: function() {
                            B.style.backgroundColor = ""
                        },duration: 300})
                }})
        }
        function r(D, A) {
            var z = document.createElement("select");
            for (var C = A, y = A * 10; C <= y; C += A) {
                var B = document.createElement("option");
                B.appendChild(document.createTextNode(C));
                z.appendChild(B)
            }
            z.style.cssText = "width:19px;margin-left:2px";
            z.onchange = function() {
                D.value = this.options[this.selectedIndex].value;
                q(D);
                n(D)
            };
            D.onblur = function() {
                q(this);
                n(this)
            };
            q(D);
            n(D);
            D.insert({after: z});
            z = null
        }
        function p() {
            e.overlayEl.removeClassName("animated");
            setTimeout(function() {
                e.overlayEl.hide()
            }, 250);
            e.modalEl.hide();
            e.modalEl.select(".wrapper").invoke("remove")
        }
        function e() {
            var B = e.modalEl = $("large-preview-modal");
            if (!B) {
                return
            }
            $$(".preview-trigger-wrapper").invoke("show");
            var D = e.overlayEl = new Element("div", {className: "large-preview-overlay",style: "display: none"});
            D.onclick = function() {
                p()
            };
            document.onkeydown = function(E) {
                if (E.keyCode === Event.KEY_ESC) {
                    p()
                } else {
                    if (E.keyCode === Event.KEY_RIGHT) {
                        if (e.modalEl.visible()) {
                            z()
                        }
                    } else {
                        if (E.keyCode === Event.KEY_LEFT) {
                            if (e.modalEl.visible()) {
                                A()
                            }
                        }
                    }
                }
            };
            var C = B.down(".next-switch"), y = B.down(".prev-switch");
            B.observe("mouseover", function() {
                if (B.select(".wrapper").length < 2) {
                    return
                }
                C.show();
                y.show()
            });
            B.observe("mouseout", function() {
                if (B.select(".wrapper").length < 2) {
                    return
                }
                C.hide();
                y.hide()
            });
            function z() {
                var F = B.select(".wrapper"), G = F.find(Element.visible), E = G.next();
                if (!E || !E.hasClassName("wrapper")) {
                    E = F[0]
                }
                G.hide();
                E.show()
            }
            function A() {
                var F = B.select(".wrapper"), G = F.find(Element.visible), E = G.previous();
                if (!E || !E.hasClassName("wrapper")) {
                    E = F.last()
                }
                G.hide();
                E.show()
            }
            C.observe("click", z);
            y.observe("click", A);
            document.body.appendChild(D);
            document.body.appendChild(B)
        }
        function x(y) {
            var z = $$(".view");
            z.each(function(B) {
                var C = B.className.replace(/product\-.*/, "").replace("view", "").trim();
                var A = document.createElement("div");
                A.className = "wrapper " + C;
                B.select(".large-preview img").each(function(D) {
                    A.appendChild(D.cloneNode(true))
                });
                e.modalEl.appendChild(A);
                A[B.visible() ? "show" : "hide"]()
            });
            e.modalEl.show();
            e.overlayEl.show();
            setTimeout(function() {
                e.overlayEl.addClassName("animated")
            }, 1)
        }
        $$(".product-quantity").each(function(z) {
            var y = parseInt(z.getAttribute("data-default-quantity"), 10);
            if (y > 1) {
                r(z, y)
            } else {
                z.observe("change", function() {
                    q(this);
                    n(this)
                })
            }
        });
        document.observe("click", function(z, y) {
            if (y = z.findElement(".closeup-color-thumbnails li a")) {
                z.stop();
                v(y);
                g()
            } else {
                if (y = z.findElement(".size-vocabulary li a")) {
                    z.stop();
                    h(y);
                    g()
                } else {
                    if (y = z.findElement(".thumb")) {
                        z.stop();
                        o(y)
                    } else {
                        if (y = z.findElement(".extra-description-toggler")) {
                            z.stop();
                            m(y)
                        } else {
                            if (y = z.findElement(".preview-trigger")) {
                                z.stop();
                                x(y)
                            } else {
                                if (y = z.findElement(".large-preview .close")) {
                                    z.stop();
                                    p()
                                }
                            }
                        }
                    }
                }
            }
        });
        $("new_comment") && $("new_comment").observe("submit", function(A) {
            var y = this.select("[required]"), B = y.any(function(D) {
                return !D.value
            }), z = this;
            if (B) {
                A.stop();
                if (z.hasAttribute("data-in-error-state")) {
                    return
                }
                var C = z.down(".error");
                z.setAttribute("data-in-error-state", "");
                C.setStyle("top:-30px").show();
                emile(C, "top:0", {duration: 500,after: function() {
                        setTimeout(function() {
                            emile(C, "top:-30px", {duration: 500,after: function() {
                                    C.hide();
                                    z.removeAttribute("data-in-error-state")
                                }})
                        }, 1000)
                    }})
            }
        })
    })
})();
