import { Link } from "@tanstack/react-router";
import { ReactNode } from "react";
import { useTheme } from "@/context/theme-context";

import { Button } from "../ui/button";
import { RxGithubLogo } from "react-icons/rx";
import { Sun, Moon, Menu } from "lucide-react";

type AppLayoutProps = {
  children: ReactNode;
};

function AppLayout({ children }: AppLayoutProps) {
  const { theme, toggleTheme } = useTheme();

  return (
    <div className="flex h-screen w-screen flex-col">
      <nav className="flex items-center justify-between px-6 py-4">
        <div className="flex items-center gap-4">
          <button className="cursor-pointer" aria-label="menu" type="button">
            <Menu className="leading-0" size="20px" />
          </button>
          <Link to="/app" className="pb-0.5 text-lg font-medium">
            appointly
          </Link>
        </div>

        <div className="flex items-center gap-1">
          <Button onClick={toggleTheme} asChild size="icon" variant="ghost">
            <a
              target="_blank"
              rel="noopener noreferrer"
              aria-label="link to developer's github profile"
              href="https://github.com/risenguyen"
            >
              <RxGithubLogo />
            </a>
          </Button>
          <Button onClick={toggleTheme} size="icon" variant="ghost">
            {theme === "light" ? <Moon /> : <Sun />}
          </Button>
        </div>
      </nav>

      <main className="flex-1">{children}</main>
    </div>
  );
}

export default AppLayout;
