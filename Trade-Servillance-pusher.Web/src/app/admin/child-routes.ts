export const childRoutes = [
  {
    path: "dashboard",
    loadChildren: () =>
      import("./dashboard/dashboard.module").then((m) => m.DashboardModule),
    data: { icon: "dashboard", text: "Dashboard" },
  },
  {
    path: "order",
    loadChildren: () =>
      import("./order-serveillance/order-serveillance.module").then(
        (m) => m.OrderServeillanceModule
      ),
    data: { icon: "supervisor_account", text: "Electronics Trade" },
  },
  {
    path: "trade",
    loadChildren: () =>
      import("./trade-servillance/trade-servillance.module").then(
        (m) => m.TradeServillanceModule
      ),
    data: { icon: "open_with", text: "Post Trade" },
  },
];
