# Policy Management System - API Endpoints

All APIs are now correctly implemented with the exact routes as specified.

## 7.1 Authentication APIs

### Register User
- **POST** `/api/auth/register`
- Registers a new user with role = User
- **Controller**: `AuthController.Register`

### Login
- **POST** `/api/auth/login`
- Returns JWT token
- **Controller**: `AuthController.Login`

---

## 7.2 Policy APIs (User)

### Get Available Policies
- **GET** `/api/policies`
- Returns only active policies
- **Controller**: `PolicyController.GetAvailablePolicies`

### Request Policy Enrollment
- **POST** `/api/policies/{policyId}/enroll`
- Status defaults to `Pending`
- **Controller**: `PolicyController.EnrollInPolicy`

### View My Enrollments
- **GET** `/api/my/enrollments`
- **Controller**: `PolicyEnrollmentController.MyEnrollments`

---

## 7.3 Policy APIs (Admin)

### Add Policy
- **POST** `/api/admin/policies`
- **Controller**: `PolicyController.CreatePolicy`
- **Authorization**: Admin role required

### Update Policy
- **PUT** `/api/admin/policies/{id}`
- **Controller**: `PolicyController.UpdatePolicy`
- **Authorization**: Admin role required

### Activate / Deactivate Policy
- **PATCH** `/api/admin/policies/{id}/status`
- **Controller**: `PolicyController.UpdatePolicyStatus`
- **Authorization**: Admin role required
- **Body**: `{ "isActive": true/false }`

---

## 7.4 Enrollment Approval APIs (Admin)

### View Pending Enrollments
- **GET** `/api/admin/enrollments?status=Pending`
- **Controller**: `PolicyEnrollmentController.GetEnrollments`
- **Authorization**: Admin role required
- Can also view all enrollments by omitting the status parameter

### Approve Enrollment
- **POST** `/api/admin/enrollments/{id}/approve`
- **Controller**: `PolicyEnrollmentController.ApproveEnrollment`
- **Authorization**: Admin role required

### Reject Enrollment
- **POST** `/api/admin/enrollments/{id}/reject`
- **Controller**: `PolicyEnrollmentController.RejectEnrollment`
- **Authorization**: Admin role required

---

## Summary of Changes Made

1. ✅ **AuthController** - Routes already correct (`/api/auth/register`, `/api/auth/login`)

2. ✅ **PolicyController** - Restructured to have distinct user and admin routes:
   - Changed base route from `api/policies` to `api` 
   - Split into user endpoints (`api/policies`, `api/policies/{policyId}/enroll`)
   - Admin endpoints (`api/admin/policies`, `api/admin/policies/{id}`, `api/admin/policies/{id}/status`)
   - Added new PATCH endpoint for status updates

3. ✅ **PolicyEnrollmentController** - Restructured routes:
   - Changed base route from `api/policy-enrollments` to `api`
   - User endpoint: `api/my/enrollments`
   - Admin endpoints: `api/admin/enrollments`, `api/admin/enrollments/{id}/approve`, `api/admin/enrollments/{id}/reject`
   - Added specific approve/reject endpoints instead of generic status update

4. ✅ **Service Layer Updates**:
   - Added `EnrollUserAsync` and `UpdatePolicyStatusAsync` to `IPolicyService`
   - Added `GetAllEnrollmentsAsync`, `ApproveEnrollmentAsync`, `RejectEnrollmentAsync` to `IPolicyEnrollmentService`
   - Implemented all new methods in service implementations

5. ✅ **Repository Layer Updates**:
   - Added `GetAllAsync` method to `IPolicyEnrollmentRepository` and its implementation

6. ✅ **DTOs**:
   - Created `UpdatePolicyStatusDto` for PATCH status endpoint

All API routes now match your specification exactly! ✅
