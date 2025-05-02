import { Link } from "@tanstack/react-router";
import { Menu } from "lucide-react";
import {
  Drawer,
  DrawerTrigger,
  DrawerContent,
  DrawerClose,
  DrawerTitle,
  DrawerDescription,
} from "../ui/drawer";

function MobileNav() {
  return (
    <Drawer autoFocus>
      <DrawerTrigger asChild>
        <button
          aria-label="Open Navigation"
          className="flex cursor-pointer items-center md:hidden"
          type="button"
        >
          <Menu className="leading-0" size="20px" />
        </button>
      </DrawerTrigger>
      <DrawerContent className="h-full">
        <DrawerTitle className="sr-only">Mobile Navigation Menu</DrawerTitle>
        <DrawerDescription className="sr-only">
          Contains links to navigate the application sections.
        </DrawerDescription>
        <nav className="flex flex-col px-8">
          <ul className="flex flex-col gap-6 py-11">
            <li className="flex">
              <DrawerClose asChild>
                <Link className="text-2xl font-medium" to="/app">
                  Dashboard
                </Link>
              </DrawerClose>
            </li>
            <li className="flex">
              <DrawerClose asChild>
                <Link className="text-2xl font-medium" to="/app/appointments">
                  Appointments
                </Link>
              </DrawerClose>
            </li>
            <li className="flex">
              <DrawerClose asChild>
                <Link className="text-2xl font-medium" to="/app/clients">
                  Clients
                </Link>
              </DrawerClose>
            </li>
            <li className="flex">
              <DrawerClose asChild>
                <Link className="text-2xl font-medium" to="/app/treatments">
                  Treatments
                </Link>
              </DrawerClose>
            </li>
            <li className="flex">
              <DrawerClose asChild>
                <Link className="text-2xl font-medium" to="/app/employees">
                  Employees
                </Link>
              </DrawerClose>
            </li>
          </ul>
        </nav>
      </DrawerContent>
    </Drawer>
  );
}

export default MobileNav;
