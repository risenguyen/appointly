import { ReactNode } from "react";

type AppLayoutProps = {
  children: ReactNode;
};

function AppLayout({ children }: AppLayoutProps) {
  return <div className="h-screen w-screen"></div>;
}

export default AppLayout;
