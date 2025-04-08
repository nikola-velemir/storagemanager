import { Route, Routes, useLocation } from "react-router-dom";
import ProtectedRoute from "../../../infrastructure/Routes/ProtectedRoute";
import Dashboard from "../../dashboard/Dashboard";
import HailFailed from "../../errors/HailFailed";
import LoginForm from "../../features/users/login/LoginForm/LoginForm";
import ContentContainer from "../ContentContainer/ContentContainer";
import { AnimatePresence } from "framer-motion";
import InvoiceSearch from "../../features/invoice/search/InvoiceSearch";
import InvoiceUpload from "../../features/invoice/upload/InvoiceUpload";
import ProviderSearch from "../../features/provider/search/ProviderSearch";
import ProviderProfile from "../../features/provider/profile/ProviderProfile";
import InvoiceInfo from "../../features/invoice/info/InvoiceInfo";
import ComponentsSearch from "../../features/component/search/ComponentsSearch";
import ComponentInfo from "../../features/component/info/ComponentInfo";

const AnimatedRoutes = () => {
  const location = useLocation();
  return (
    <AnimatePresence>
      <Routes location={location} key={location.pathname}>
        <Route path="/login" element={<LoginForm />} />
        <Route element={<ProtectedRoute />}>
          <Route
            path="/"
            element={
              <ContentContainer>
                <Dashboard />
              </ContentContainer>
            }
          ></Route>
          <Route
            path="/components-search"
            element={
              <ContentContainer>
                <ComponentsSearch />
              </ContentContainer>
            }
          />
          <Route
            path="/component-info/:id"
            element={
              <ContentContainer>
                <ComponentInfo />
              </ContentContainer>
            }
          />
          <Route
            path="/invoice-search"
            element={
              <ContentContainer>
                <InvoiceSearch></InvoiceSearch>
              </ContentContainer>
            }
          />
          <Route
            path="/invoice-upload"
            element={
              <ContentContainer>
                <InvoiceUpload />
              </ContentContainer>
            }
          />
          <Route
            path="/provider-profile/:id"
            element={
              <ContentContainer>
                <ProviderProfile />
              </ContentContainer>
            }
          />
          <Route
            path="/invoice-info/:id"
            element={
              <ContentContainer>
                <InvoiceInfo />
              </ContentContainer>
            }
          />
          <Route
            path="/providers-search"
            element={
              <ContentContainer>
                <ProviderSearch />
              </ContentContainer>
            }
          />
          <Route
            path="/kurac"
            element={
              <ContentContainer>
                <HailFailed />
              </ContentContainer>
            }
          />
        </Route>
        <Route path="/hailFailed" element={<HailFailed />} />
      </Routes>
    </AnimatePresence>
  );
};

export default AnimatedRoutes;
