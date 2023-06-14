import { TopBar } from './TopBar'
import { Outlet } from 'react-router-dom'

export const Layout = () => {
  return (
    <>
      <TopBar />
      <Outlet />
    </>
  )
}
