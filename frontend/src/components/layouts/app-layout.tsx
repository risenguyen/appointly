import { Link } from "@tanstack/react-router";
import { ReactNode } from "react";
import { useTheme } from "@/context/theme-context";
import { RxGithubLogo } from "react-icons/rx";
import { Sun, Moon } from "lucide-react";
import { Button } from "../ui/button";
import MobileNav from "./mobile-nav";

type AppLayoutProps = {
  children: ReactNode;
};

function AppLayout({ children }: AppLayoutProps) {
  const { theme, toggleTheme } = useTheme();

  return (
    <div className="flex h-screen w-screen flex-col">
      <header className="flex items-center justify-between px-5 py-5">
        <div className="flex items-center gap-4 pl-1.5">
          <MobileNav />
          <Link to="/app" className="pb-0.5 text-xl font-medium">
            appointly
          </Link>
        </div>

        <div className="flex items-center gap-1">
          <Button
            aria-label="Link to developer's GitHub"
            onClick={toggleTheme}
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
