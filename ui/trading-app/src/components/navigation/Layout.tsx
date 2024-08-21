import { Outlet } from "react-router-dom";
import { TopBar } from "./TopBar";

export const Layout = () => (
  <>
    <TopBar />
    <Outlet />
  </>
);
