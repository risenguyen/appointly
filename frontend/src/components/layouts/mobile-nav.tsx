import { Link } from "@tanstack/react-router";
import { TbMenu } from "react-icons/tb";
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
          <TbMenu className="leading-0" size="20px" />
        </button>
      </DrawerTrigger>
      <DrawerContent className="">
        <DrawerTitle className="sr-only">Mobile Navigation Menu</DrawerTitle>
        <DrawerDescription className="sr-only">
          Contains links to navigate the application sections.
        </DrawerDescription>
        <nav className="flex flex-col px-8">
          <ul className="flex flex-col gap-6 py-12">
            <li className="flex">
              <DrawerClose asChild>
                <Link
                  activeOptions={{
                    exact: true,
                  }}
                  className="data-[status=active]:text-foreground text-muted-foreground hover:text-foreground text-2xl font-medium transition-colors"
                  to="/app"
                >
                  Dashboard
                </Link>
              </DrawerClose>
            </li>
            <li className="flex">
              <DrawerClose asChild>
                <Link
                  className="data-[status=active]:text-foreground text-muted-foreground hover:text-foreground text-2xl font-medium transition-colors"
                  to="/app/appointments"
                >
                  Appointments
                </Link>
              </DrawerClose>
            </li>
            <li className="flex">
              <DrawerClose asChild>
                <Link
                  className="data-[status=active]:text-foreground text-muted-foreground hover:text-foreground text-2xl font-medium transition-colors"
                  to="/app/clients"
                >
                  Clients
                </Link>
              </DrawerClose>
            </li>
            <li className="flex">
              <DrawerClose asChild>
                <Link
                  className="data-[status=active]:text-foreground text-muted-foreground hover:text-foreground text-2xl font-medium transition-colors"
                  to="/app/treatments"
                >
                  Treatments
                </Link>
              </DrawerClose>
            </li>
            <li className="flex">
              <DrawerClose asChild>
                <Link
                  className="data-[status=active]:text-foreground text-muted-foreground hover:text-foreground text-2xl font-medium transition-colors"
                  to="/app/employees"
                >
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
