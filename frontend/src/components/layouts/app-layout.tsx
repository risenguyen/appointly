import { Link } from "@tanstack/react-router";
import type { NavLink } from "@/lib/tanstack-router";

import { useMemo, type ReactNode } from "react";
import { useTheme } from "@/context/theme-context";

import { TbMenu } from "react-icons/tb";
import { RxGithubLogo } from "react-icons/rx";
import { Sun, Moon } from "lucide-react";

import { Button } from "../ui/button";
import {
  Drawer,
  DrawerTrigger,
  DrawerContent,
  DrawerTitle,
  DrawerDescription,
} from "../ui/drawer";

import MobileNav from "./mobile-nav";
import DesktopNav from "./desktop-nav";

type AppLayoutProps = {
  children: ReactNode;
};

function AppLayout({ children }: AppLayoutProps) {
  const { theme, toggleTheme } = useTheme();

  const navLinks = useMemo<Array<NavLink>>(
    () => [
      {
        to: "/app",
        activeOptions: {
          exact: true,
        },
        label: "Dashboard",
      },
      {
        to: "/app/appointments",
        label: "Appointments",
      },
      {
        to: "/app/clients",
        label: "Clients",
      },
      {
        to: "/app/treatments",
        label: "Treatments",
      },
      {
        to: "/app/employees",
        label: "Employees",
      },
    ],
    [],
  );

  return (
    <div className="flex w-screen justify-center">
      <div className="container mx-auto flex min-h-screen flex-col">
        <header className="flex items-center justify-between px-7 py-6 md:px-8 lg:px-10">
          <div className="flex items-center gap-4 pl-1 md:pl-2">
            <Drawer autoFocus>
              <DrawerTrigger asChild>
                <button
                  aria-label="Open Navigation"
                  className="flex cursor-pointer items-center md:hidden"
                  type="button"
                >
                  <TbMenu className="leading-0" size="20px" />
                </button>
              </DrawerTrigger>
              <DrawerContent>
                <DrawerTitle className="sr-only">
                  Mobile Navigation Menu
                </DrawerTitle>
                <DrawerDescription className="sr-only">
                  Contains links to navigate the application sections.
                </DrawerDescription>
                <MobileNav navLinks={navLinks} />
              </DrawerContent>
            </Drawer>
            <Link to="/app" className="pb-1 text-xl font-medium">
              appointly
            </Link>
            <DesktopNav navLinks={navLinks} />
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
    </div>
  );
}

export default AppLayout;
