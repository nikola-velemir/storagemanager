import { Route, Routes, useLocation } from "react-router-dom";
import ProtectedRoute from "../../../infrastructure/Routes/ProtectedRoute";
import Dashboard from "../../dashboard/Dashboard";
import HailFailed from "../../errors/HailFailed";
import LoginForm from "../../features/users/login/LoginForm/LoginForm";
import ContentContainer from "../ContentContainer/ContentContainer";
import { AnimatePresence } from "framer-motion";
import ComponentsSearch from "../../features/component/search/ComponentsSearch";
import ComponentInfo from "../../features/component/info/ComponentInfo";
import { RouteStackProvider } from "../../../infrastructure/Routes/RouteStackContext";
import ProductCreatePage from "../../features/product/create/ProductCreatePage";
import ProductSearch from "../../features/product/search/ProductSearch";
import ProductInfo from "../../features/product/info/ProductInfo";
import ExportCreatePage from "../../features/invoice/export/create/ExportCreatePage";
import ImportInfo from "../../features/invoice/import/info/ImportInfo";
import ImportUpload from "../../features/invoice/import/upload/ImportUpload";
import BusinessPartnersSearch from "../../features/businessPartners/search/BusinessPartnersSearch";
import InvoiceSearch from "../../features/invoice/search/InvoiceSearch";
import BusinessPartnerCreatePage from "../../features/businessPartners/create/BusinessPartnerCreatePage";
import BusinessPartnerProfile from "../../features/businessPartners/profile/BusinessPartnerProfile";

const AnimatedRoutes = () => {
  const location = useLocation();
  return (
    <AnimatePresence>
      <RouteStackProvider>
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
                  <InvoiceSearch />
                </ContentContainer>
              }
            />
            <Route
              path="/import-upload"
              element={
                <ContentContainer>
                  <ImportUpload />
                </ContentContainer>
              }
            />
            <Route
              path="/product-search"
              element={
                <ContentContainer>
                  <ProductSearch />
                </ContentContainer>
              }
            />
            <Route
              path="/product-info/:id"
              element={
                <ContentContainer>
                  <ProductInfo />
                </ContentContainer>
              }
            />
            <Route
              path="/partner-profile/:id"
              element={
                <ContentContainer>
                  <BusinessPartnerProfile />
                </ContentContainer>
              }
            />
            <Route
              path="/create-product"
              element={
                <ContentContainer>
                  <ProductCreatePage />
                </ContentContainer>
              }
            />
            <Route
              path="/invoice-info/:id"
              element={
                <ContentContainer>
                  <ImportInfo />
                </ContentContainer>
              }
            />
            <Route
              path="/business-partner-search"
              element={
                <ContentContainer>
                  <BusinessPartnersSearch />
                </ContentContainer>
              }
            />
            <Route
              path="/export"
              element={
                <ContentContainer>
                  <ExportCreatePage />
                </ContentContainer>
              }
            />
            <Route
              path="/create-partner"
              element={
                <ContentContainer>
                  <BusinessPartnerCreatePage />
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
      </RouteStackProvider>
    </AnimatePresence>
  );
};

export default AnimatedRoutes;
