import { Route, Routes } from "react-router";
import Home from "./pages/Home";
import MainLayout from "./layouts/MainLayout";
import "./styles/main.css"
import NotFound from "./pages/NotFound";
import CarShiftOpenPage from "./pages/carShift/Open";
import UsersPage from "./pages/users/Users";
import UserPage from "./pages/users/User";
import CarShiftClosePage from "./pages/carShift/Close";
import CarShiftInfoPage from "./pages/carShift/Info";
import StylesPage from "./pages/test/Styles";

export default function App() {
    return (
        <Routes>
            <Route element={<MainLayout />}>
                <Route index element={<Home />} />
                <Route path="*" element={<NotFound />} />

                <Route path="/shifts/cars/:id" element={<CarShiftInfoPage />} />
                <Route path="/shifts/cars/:id/close" element={<CarShiftClosePage />} />
                <Route path="/shifts/cars/open" element={<CarShiftOpenPage />} />

                <Route path="/test/styles" element={<StylesPage />} />

                <Route path="/users" element={<UsersPage />} />
                <Route path="/users/:id" element={<UserPage />} />
            </Route>
        </Routes>
    );
}