import { TopBar } from './TopBar'
import { SideBar } from './SideBar'
import { Outlet } from 'react-router-dom'

export const Layout = () => {
  return (
    <>
      <TopBar />
      <SideBar />
      <Outlet />
    </>
  )
}
