import { ReactNode } from "react";

type AppLayoutProps = {
  children: ReactNode;
};

function AppLayout({ children }: AppLayoutProps) {
  return <div>{children}</div>;
}

export default AppLayout;
