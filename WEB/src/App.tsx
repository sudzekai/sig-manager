import { Route, Routes } from "react-router";
import Home from "./pages/Home";
import MainLayout from "./layouts/MainLayout";
import "./styles/main.css"
import NotFound from "./pages/NotFound";
import StylesPage from "./pages/test/Styles";

import CarShiftInfoPage from "./pages/shifts/carShift/Info";
import CarShiftClosePage from "./pages/shifts/carShift/Close";
import CarShiftOpenPage from "./pages/shifts/carShift/Open";
import UserInfoPage from "./pages/users/Info";
import UserListPage from "./pages/users/List";
import ShiftsOpenRouterPage from "./pages/shifts/OpenRouter";
import UserCreatePage from "./pages/users/Create";
import ShiftsListRouterPage from "./pages/shifts/ListRouter";
import CarShiftListPage from "./pages/shifts/carShift/List";

export default function App() {
    return (
        <Routes>
            <Route element={<MainLayout />}>
                <Route index element={<Home />} />
                <Route path="*" element={<NotFound />} />

                <Route path="/shifts/open/router" element={<ShiftsOpenRouterPage />} />
                <Route path="/shifts/router" element={<ShiftsListRouterPage/>} />

                <Route path="/shifts/cars" element={<CarShiftListPage/>} />
                <Route path="/shifts/cars/:id" element={<CarShiftInfoPage />} />
                <Route path="/shifts/cars/:id/close" element={<CarShiftClosePage />} />
                <Route path="/shifts/cars/open" element={<CarShiftOpenPage />} />

                <Route path="/test/styles" element={<StylesPage />} />

                <Route path="/register" element={<UserCreatePage />} />
                <Route path="/login" element={<NotFound />} />

                <Route path="/users" element={<UserListPage />} />
                <Route path="/users/:id" element={<UserInfoPage />} />
            </Route>
        </Routes>
    );
}