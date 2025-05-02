import { Link } from "@tanstack/react-router";

function DesktopNav() {
  return (
    <nav className="ml-3 hidden md:flex">
      <ul className="flex items-center gap-7">
        <li className="flex">
          <Link
            activeOptions={{
              exact: true,
            }}
            className="text-muted-foreground hover:text-foreground data-[status=active]:text-foreground transition-colors"
            to="/app"
          >
            Dashboard
          </Link>
        </li>
        <li className="flex">
          <Link
            className="text-muted-foreground hover:text-foreground data-[status=active]:text-foreground dtransition-colors"
            to="/app"
          >
            Appointments
          </Link>
        </li>
        <li className="flex">
          <Link
            className="text-muted-foreground hover:text-foreground data-[status=active]:text-foreground transition-colors"
            to="/app"
          >
            Clients
          </Link>
        </li>
        <li className="flex">
          <Link
            className="text-muted-foreground hover:text-foreground data-[status=active]:text-foreground transition-colors"
            to="/app"
          >
            Treatments
          </Link>
        </li>
        <li className="flex">
          <Link
            className="text-muted-foreground hover:text-foreground data-[status=active]:text-foreground transition-colors"
            to="/app"
          >
            Employees
          </Link>
        </li>
      </ul>
    </nav>
  );
}

export default DesktopNav;
