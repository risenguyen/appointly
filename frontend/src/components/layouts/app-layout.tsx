import { Link } from "@tanstack/react-router";
import { ReactNode } from "react";
import { useTheme } from "@/context/theme-context";
import { RxGithubLogo } from "react-icons/rx";
import { Sun, Moon } from "lucide-react";
import { Button } from "../ui/button";
import MobileNav from "./mobile-nav";
import DesktopNav from "./desktop-nav";

type AppLayoutProps = {
  children: ReactNode;
};

function AppLayout({ children }: AppLayoutProps) {
  const { theme, toggleTheme } = useTheme();

  return (
    <div className="flex h-screen w-screen flex-col">
      <header className="flex items-center justify-between px-6 py-6 md:px-6 lg:px-8">
        <div className="flex items-center gap-4 pl-1.5">
          <MobileNav />
          <Link to="/app" className="pb-1 text-xl font-medium">
            appointly
          </Link>
          <DesktopNav />
        </div>

        <div className="flex items-center gap-1">
          <Button
            aria-label="Link to developer's GitHub"
            asChild
            size="icon"
            variant="ghost"
          >
            <a
              target="_blank"
              rel="noopener noreferrer"
              aria-label="link to developer's github profile"
              href="https://github.com/risenguyen"
            >
              <RxGithubLogo />
            </a>
          </Button>
          <Button
            aria-label="Toggle Dark Mode"
            onClick={toggleTheme}
            size="icon"
            variant="ghost"
          >
            {theme === "light" ? <Moon /> : <Sun />}
          </Button>
        </div>
      </header>

      <main className="flex-1">{children}</main>
    </div>
  );
}

export default AppLayout;
