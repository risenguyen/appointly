import { ReactNode } from "react";

type AppLayoutProps = {
  children: ReactNode;
};

function AppLayout({ children }: AppLayoutProps) {
  return children;
}

export default AppLayout;
