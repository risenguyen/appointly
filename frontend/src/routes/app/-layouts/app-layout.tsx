import { Link, useNavigate } from "@tanstack/react-router";
import type { NavLink } from "@/lib/tanstack-router";

import { useMemo, type ReactNode } from "react";
import { useAuth } from "@/context/auth-context";
import { useTheme } from "@/context/theme-context";

import { TbMenu } from "react-icons/tb";
import { RxGithubLogo } from "react-icons/rx";
import { Sun, Moon, LogOut } from "lucide-react";

import { Button } from "@/components/ui/button";
import { ScrollArea } from "@/components/ui/scroll-area";
import {
  Drawer,
  DrawerTrigger,
  DrawerContent,
  DrawerTitle,
  DrawerDescription,
  DrawerClose,
} from "@/components/ui/drawer";
import {
  AlertDialog,
  AlertDialogCancel,
  AlertDialogAction,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "@/components/ui/alert-dialog";

function MobileNav({ navLinks }: { navLinks: Array<NavLink> }) {
  return (
    <nav className="flex flex-col px-8">
      <ul className="flex flex-col gap-6 py-8">
        {navLinks.map((navLink) => (
          <li key={navLink.to} className="flex">
            <DrawerClose asChild>
              <Link
                className="data-[status=active]:text-foreground focus-visible:text-foreground text-muted-foreground hover:text-foreground text-2xl font-medium transition-colors"
                {...navLink}
              >
                {navLink.label}
              </Link>
            </DrawerClose>
          </li>
        ))}
      </ul>
    </nav>
  );
}

function DesktopNav({ navLinks }: { navLinks: Array<NavLink> }) {
  return (
    <nav className="ml-4 hidden md:flex">
      <ul className="flex items-center gap-7">
        {navLinks.map((navLink) => (
          <li key={navLink.to} className="flex">
            <Link
              className="text-muted-foreground hover:text-foreground focus-visible:text-foreground data-[status=active]:text-foreground transition-colors"
              {...navLink}
            >
              {navLink.label}
            </Link>
          </li>
        ))}
      </ul>
    </nav>
  );
}

function AppLayout({ children }: { children: ReactNode }) {
  const { logout } = useAuth();
  const { theme, toggleTheme } = useTheme();
  const navigate = useNavigate();

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
    <ScrollArea className="h-screen transition-all" type="scroll">
      <div className="container mx-auto flex min-h-screen flex-col">
        <header className="flex items-center justify-between p-8">
          <div className="flex items-center gap-4">
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

          <div className="flex items-center gap-0.5">
            <Button
              aria-label="Visit Developer's Github Profile"
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
              {theme === "light" ? <Sun /> : <Moon />}
            </Button>
            <AlertDialog>
              <AlertDialogTrigger asChild>
                <Button aria-label="Log Out" size="icon" variant="ghost">
                  <LogOut />
                </Button>
              </AlertDialogTrigger>
              <AlertDialogContent>
                <AlertDialogHeader>
                  <AlertDialogTitle>Log Out?</AlertDialogTitle>
                  <AlertDialogDescription>
                    Are you sure you want to log out? You will need to sign in
                    again to access your account.
                  </AlertDialogDescription>
                </AlertDialogHeader>
                <AlertDialogFooter>
                  <AlertDialogCancel>Cancel</AlertDialogCancel>
                  <AlertDialogAction
                    onClick={() => {
                      logout();
                      navigate({
                        to: "/login",
                      });
                    }}
                  >
                    Log Out
                  </AlertDialogAction>
                </AlertDialogFooter>
              </AlertDialogContent>
            </AlertDialog>
          </div>
        </header>

        <main className="flex flex-1 flex-col px-8 pt-2 pb-8">{children}</main>
      </div>
    </ScrollArea>
  );
}

export default AppLayout;
